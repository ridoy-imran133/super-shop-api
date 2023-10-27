using SuperShop.Entities;
using SuperShop.Helper;
using SuperShop.Models;
using SuperShop.Models.DTO;
using SuperShop.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Repository.Implementation
{
    public class OutletRepository : IOutletRepository
    {
        public async Task<List<Outlet>> GetAllOutlet(SuperShopDBContext pContext)
        {
            return pContext.Outlet.ToList();
        }

        public async Task<Outlet> GetOutlet(string pOutletCode, SuperShopDBContext pContext)
        {
            return pContext.Outlet.Where(x => x.OutletCode == pOutletCode).FirstOrDefault();
        }

        public async Task<ApiResponseModel> SaveOutlet(Outlet poutlet, SuperShopDBContext pContext)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                Outlet outlet = pContext.Outlet.Where(x => x.OutletCode == poutlet.OutletCode).FirstOrDefault();
                if (outlet == null)
                {
                    var count = ((pContext.Outlet.Count()) + 1).ToString();
                    count = count.Length > 3 ? count : ("0000" + count).Substring(("0000" + count).Length - 4);
                    poutlet.OutletCode = "OUT" + count;
                    poutlet.IsActive = "Y";
                    poutlet.IsDelete = "N";
                    poutlet.CreatedBy = "system";
                    poutlet.CreatedDate = DateTime.Now;

                    await pContext.AddAsync(poutlet);
                    await pContext.SaveChangesAsync();
                    response.ResponseCode = StaticValue.SuccessCode;
                    response.ResponseMessage = "Saved Successfully";
                    response.ResponseData = poutlet;
                }
                else
                {
                    outlet.OutletName = poutlet.OutletName;
                    outlet.Address = poutlet.Address;
                    outlet.Mobile = poutlet.Mobile;
                    outlet.ModifiedBy = "system";
                    outlet.ModifiedDate = DateTime.Now;

                    pContext.Update(outlet);
                    await pContext.SaveChangesAsync();
                    response.ResponseCode = StaticValue.SuccessCode;
                    response.ResponseMessage = "Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = StaticValue.Unauthorized;
                response.ResponseMessage = ex.Message;
            }
            return response;
        }

        public async Task<Outlet> Delete(string pOutletCode, SuperShopDBContext pContext)
        {
            var val = pContext.Outlet.Where(x => x.OutletCode == pOutletCode).FirstOrDefault();
            pContext.Outlet.Remove(val);
            await pContext.SaveChangesAsync();
            return val;
        }

        public async Task<List<List<OutletWiseProductDTO>>> GetOutletWiseProducts(string pOutletCode, SuperShopDBContext pContext)
        {
            return ((from a in pContext.Product
                     join b in pContext.OutletWiseProduct on a.ProductCode equals b.ProductCode
                     join c in pContext.Category on b.CatCode equals c.CatCode
                     where b.OutletCode == pOutletCode
                     select new OutletWiseProductDTO
                     {
                         CatCode = a.CatCode,
                         CatName = c.CategoryName,
                         ProductCode = a.ProductCode,
                         ProductName = a.ProductName,
                         IsAvailable = true,
                     }).ToList().Concat((from d in pContext.Product.Where(a => !pContext.OutletWiseProduct.Any(b => a.ProductCode == b.ProductCode && b.OutletCode == pOutletCode))
                                         join e in pContext.Category on d.CatCode equals e.CatCode
                                         select new OutletWiseProductDTO
                                         {
                                             CatCode = d.CatCode,
                                             CatName = e.CategoryName,
                                             ProductCode = d.ProductCode,
                                             ProductName = d.ProductName,
                                             IsAvailable = false,
                                         }).ToList()).ToList()).GroupBy(u => u.CatCode)
                                            .Select(grp => grp.ToList())
                                            .ToList();
        }
    }
}

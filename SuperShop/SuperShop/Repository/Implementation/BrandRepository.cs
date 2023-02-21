using SuperShop.Entities;
using SuperShop.Helper;
using SuperShop.Models;
using SuperShop.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Repository.Implementation
{
    public class BrandRepository : IBrandRepository
    {
        public async Task<List<Brand>> GetAllBrand(SuperShopDBContext pContext)
        {
            return pContext.Brand.ToList();
        }

        public async Task<Brand> GetBrand(string pBrandCode, SuperShopDBContext pContext)
        {
            return pContext.Brand.Where(x => x.BrandCode == pBrandCode).FirstOrDefault();
        }

        public async Task<ApiResponseModel> SaveBrand(Brand pbrand, SuperShopDBContext pContext)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                Brand brand = pContext.Brand.Where(x => x.BrandCode == pbrand.BrandCode).FirstOrDefault();
                if (brand == null)
                {
                    var count = ((pContext.Brand.Count()) + 1).ToString();
                    count = count.Length > 3 ? count : ("0000" + count).Substring(("0000" + count).Length - 4);
                    pbrand.BrandCode = "BR"+count;
                    pbrand.IsActive = "Y";
                    pbrand.IsDelete = "N";
                    pbrand.CreatedBy = "system";
                    pbrand.CreatedDate = DateTime.Now;

                    await pContext.AddAsync(pbrand);
                    await pContext.SaveChangesAsync();
                    response.ResponseCode = StaticValue.SuccessCode;
                    response.ResponseMessage = "Saved Successfully";
                    response.ResponseData = pbrand;
                }
                else
                {
                    brand.BrandName = pbrand.BrandName;
                    brand.ModifiedBy = "system";
                    brand.ModifiedDate = DateTime.Now;

                    pContext.Update(brand);
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

        public async Task<Brand> Delete(string pBrandCode, SuperShopDBContext pContext)
        {
            var val = pContext.Brand.Where(x => x.BrandCode == pBrandCode).FirstOrDefault();
            pContext.Brand.Remove(val);
            await pContext.SaveChangesAsync();
            return val;
        }
    }
}

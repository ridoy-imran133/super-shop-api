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
    public class QtyTypeRepository : IQtyTypeRepository
    {
        public async Task<List<QtyType>> GetAllQtyType(SuperShopDBContext pContext)
        {
            return pContext.QtyType.ToList();
        }

        public async Task<QtyType> GetQtyType(string pQtyTypeCode, SuperShopDBContext pContext)
        {
            return pContext.QtyType.Where(x => x.QtyTypeCode == pQtyTypeCode).FirstOrDefault();
        }

        public async Task<ApiResponseModel> SaveQtyType(QtyType pqtytype, SuperShopDBContext pContext)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                QtyType qtytype = pContext.QtyType.Where(x => x.QtyTypeCode == pqtytype.QtyTypeCode).FirstOrDefault();
                if (qtytype == null)
                {
                    var count = ((pContext.QtyType.Count()) + 1).ToString();
                    count = count.Length > 3 ? count : ("0000" + count).Substring(("0000" + count).Length - 4);
                    pqtytype.QtyTypeCode = "QT"+count;
                    pqtytype.IsActive = "Y";
                    pqtytype.IsDelete = "N";
                    pqtytype.CreatedBy = "system";
                    pqtytype.CreatedDate = DateTime.Now;

                    await pContext.AddAsync(pqtytype);
                    await pContext.SaveChangesAsync();
                    response.ResponseCode = StaticValue.SuccessCode;
                    response.ResponseMessage = "Saved Successfully";
                    response.ResponseData = pqtytype;
                }
                else
                {
                    qtytype.QtyTypeName = pqtytype.QtyTypeName;
                    qtytype.ModifiedBy = "system";
                    qtytype.ModifiedDate = DateTime.Now;

                    pContext.Update(qtytype);
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

        public async Task<QtyType> Delete(string pQtyTypeCode, SuperShopDBContext pContext)
        {
            var val = pContext.QtyType.Where(x => x.QtyTypeCode == pQtyTypeCode).FirstOrDefault();
            pContext.QtyType.Remove(val);
            await pContext.SaveChangesAsync();
            return val;
        }
    }
}

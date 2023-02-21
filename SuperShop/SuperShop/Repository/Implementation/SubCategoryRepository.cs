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
    public class SubCategoryRepository : ISubCategoryRepository
    {
        public async Task<List<SubCategory>> GetAllSubCategory(SuperShopDBContext pContext)
        {
            return pContext.SubCategory.ToList();
        }

        public async Task<SubCategory> GetSubCategory(string pSubCatCode, SuperShopDBContext pContext)
        {
            return pContext.SubCategory.Where(x => x.SubCatCode == pSubCatCode).FirstOrDefault();
        }

        public async Task<ApiResponseModel> SaveSubCategory(SubCategory pSubCategory, SuperShopDBContext pContext)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                SubCategory subcategory = pContext.SubCategory.Where(x => x.SubCatCode == pSubCategory.SubCatCode).FirstOrDefault();
                if (subcategory == null)
                {
                    var count = ((pContext.SubCategory.Count()) + 1).ToString();
                    count = count.Length > 3 ? count : ("0000" + count).Substring(("0000" + count).Length - 4);
                    pSubCategory.SubCatCode = "SCAT"+count;
                    pSubCategory.IsActive = "Y";
                    pSubCategory.IsDelete = "N";
                    pSubCategory.CreatedBy = "system";
                    pSubCategory.CreatedDate = DateTime.Now;

                    await pContext.AddAsync(pSubCategory);
                    await pContext.SaveChangesAsync();
                    response.ResponseCode = StaticValue.SuccessCode;
                    response.ResponseMessage = "Saved Successfully";
                    response.ResponseData = pSubCategory;
                }
                else
                {
                    subcategory.SubCategoryName = pSubCategory.SubCategoryName;
                    subcategory.CatCode = pSubCategory.CatCode;
                    subcategory.ModifiedBy = "system";
                    subcategory.ModifiedDate = DateTime.Now;

                    pContext.Update(subcategory);
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

        public async Task<SubCategory> DeleteData(string pSubCatCode, SuperShopDBContext pContext)
        {
            var val = pContext.SubCategory.Where(x => x.SubCatCode == pSubCatCode).FirstOrDefault();
            pContext.SubCategory.Remove(val);
            await pContext.SaveChangesAsync();
            return val;
        }
    }
}

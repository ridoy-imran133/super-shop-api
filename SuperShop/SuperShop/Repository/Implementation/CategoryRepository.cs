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
    public class CategoryRepository : ICategoryRepository
    {
        public async Task<List<Category>> GetAllCategory(SuperShopDBContext pContext)
        {
            return pContext.Category.ToList();
        }

        public async Task<Category> GetCategory(string pCatCode,SuperShopDBContext pContext)
        {
            return pContext.Category.Where(x => x.CatCode == pCatCode).FirstOrDefault();
        }

        public async Task<ApiResponseModel> SaveCategory(Category pcategory,SuperShopDBContext pContext)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                Category category = pContext.Category.Where(x => x.CatCode == pcategory.CatCode).FirstOrDefault();
                if (category == null)
                {
                    var count = ((pContext.Category.Count()) + 1).ToString();
                    count = count.Length > 3 ? count : ("0000" + count).Substring(("0000" + count).Length - 4);
                    pcategory.CatCode = "CAT"+count;
                    pcategory.IsActive = "Y";
                    pcategory.IsDelete = "N";
                    pcategory.CreatedBy = "system";
                    pcategory.CreatedDate = DateTime.Now;

                    await pContext.AddAsync(pcategory);
                    await pContext.SaveChangesAsync();
                    response.ResponseCode = StaticValue.SuccessCode;
                    response.ResponseMessage = "Saved Successfully";
                    response.ResponseData = pcategory;
                }
                else
                {
                    category.CategoryName = pcategory.CategoryName;
                    category.ModifiedBy = "system";
                    category.ModifiedDate = DateTime.Now;

                    pContext.Update(category);
                    await pContext.SaveChangesAsync();
                    response.ResponseCode = StaticValue.SuccessCode;
                    response.ResponseMessage = "Updated Successfully";
                    response.ResponseData = category;
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = StaticValue.Unauthorized;
                response.ResponseMessage = ex.Message;
            }
            return response;
        }

        public async Task<Category> DeleteData(string pCatCode, SuperShopDBContext pContext)
        {
            var val = pContext.Category.Where(x => x.CatCode == pCatCode).FirstOrDefault();
            pContext.Category.Remove(val);
            await pContext.SaveChangesAsync();
            return val;
        }
    }
}

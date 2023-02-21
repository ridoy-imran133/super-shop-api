using SuperShop.Entities;
using SuperShop.Helper;
using SuperShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Repository.Interface
{
    public interface ISubCategoryRepository
    {
        Task<List<SubCategory>> GetAllSubCategory(SuperShopDBContext pContext);
        Task<SubCategory> GetSubCategory(string pSubCatCode, SuperShopDBContext pContext);
        Task<ApiResponseModel> SaveSubCategory(SubCategory pSubCategory, SuperShopDBContext pContext);
        Task<SubCategory> DeleteData(string pSubCatCode, SuperShopDBContext pContext);
    }
}

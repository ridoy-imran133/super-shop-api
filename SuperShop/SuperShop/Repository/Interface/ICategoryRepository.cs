using SuperShop.Entities;
using SuperShop.Helper;
using SuperShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Repository.Interface
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategory(SuperShopDBContext pContext);
        Task<Category> GetCategory(string pCatCode, SuperShopDBContext pContext);
        Task<ApiResponseModel> SaveCategory(Category pcategory, SuperShopDBContext pContext);
        Task<Category> DeleteData(string pCatCode, SuperShopDBContext pContext);
    }
}

using SuperShop.Models;
using SuperShop.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Services.Interface
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetAllCategory();
        Task<ApiResponseModel> GetCategory(String pCatCode);
        Task<ApiResponseModel> SaveCategory(CategoryDTO pCategory);
        Task<ApiResponseModel> DeleteCategory(String pCatCode);
    }
}

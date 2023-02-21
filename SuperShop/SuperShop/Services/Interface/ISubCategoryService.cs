using SuperShop.Models;
using SuperShop.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Services.Interface
{
    public interface ISubCategoryService
    {
        Task<List<SubCategoryDTO>> GetAllSubCategory();
        Task<ApiResponseModel> GetSubCategory(String pSubCatCode);
        Task<ApiResponseModel> SaveSubCategory(SubCategoryDTO pSubCategory);
        Task<ApiResponseModel> DeleteSubCategory(String pSubCatCode);
    }
}

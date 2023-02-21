using SuperShop.Models;
using SuperShop.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Services.Interface
{
    public interface IBrandService
    {
        Task<List<BrandDTO>> GetAllBrand();
        Task<ApiResponseModel> GetBrand(String pBrandCode);
        Task<ApiResponseModel> SaveBrand(BrandDTO pBrand);
        Task<ApiResponseModel> DeleteBrand(String pBrandCode);
    }
}

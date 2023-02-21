using SuperShop.Entities;
using SuperShop.Helper;
using SuperShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Repository.Interface
{
    public interface IBrandRepository
    {
        Task<List<Brand>> GetAllBrand(SuperShopDBContext pContext);
        Task<Brand> GetBrand(string pBrandCode, SuperShopDBContext pContext);
        Task<ApiResponseModel> SaveBrand(Brand pbrand, SuperShopDBContext pContext);
        Task<Brand> Delete(string pBrandCode, SuperShopDBContext pContext);
    }
}

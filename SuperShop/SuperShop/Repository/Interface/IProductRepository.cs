using SuperShop.Entities;
using SuperShop.Helper;
using SuperShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Repository.Interface
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProduct(SuperShopDBContext pContext);
        Task<Product> GetProduct(int pProductCode, SuperShopDBContext pContext);
        Task<ApiResponseModel> SaveProduct(Product pbrand, List<UploadFileModel> uploadfiles, SuperShopDBContext pContext);
        Task<Product> Delete(int pProductCode, SuperShopDBContext pContext);
        //Task<bool> SaveProductImage(List<UploadFileModel> uploadfiles, int productid, SuperShopDBContext pContext);
    }
}

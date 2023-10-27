using SuperShop.Helper;
using SuperShop.Models;
using SuperShop.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Services.Interface
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetAllProduct();
        Task<List<List<ProductDTO>>> GetcATAllProduct();
        Task<Dictionary<string, List<ProductDTO>>> GetcATAllProductDataDic();
        Task<ApiResponseModel> GetProduct(int pProductCode);
        Task<ApiResponseModel> SaveProduct(ProductDTO pProduct, List<UploadFileModel> uploadfiles);
        Task<ApiResponseModel> DeleteProduct(int pProductCode);
    }
}

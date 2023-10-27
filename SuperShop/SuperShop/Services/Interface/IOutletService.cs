using SuperShop.Models;
using SuperShop.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Services.Interface
{
    public interface IOutletService
    {
        Task<List<OutletDTO>> GetAllOutlet();
        Task<ApiResponseModel> GetOutlet(String pOutletCode);
        Task<ApiResponseModel> SaveOutlet(OutletDTO pOutlet);
        Task<ApiResponseModel> DeleteOutlet(String pOutletCode);
        Task<List<List<OutletWiseProductDTO>>> GetOutletWiseProducts(string pOutletCode);
    }
}

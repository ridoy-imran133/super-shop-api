using SuperShop.Models;
using SuperShop.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Services.Interface
{
    public interface IQtyTypeService
    {
        Task<List<QtyTypeDTO>> GetAllQtyType();
        Task<ApiResponseModel> GetQtyType(String pQtyTypeCode);
        Task<ApiResponseModel> SaveQtyType(QtyTypeDTO pQtyType);
        Task<ApiResponseModel> DeleteQtyType(String pQtyTypeCode);
    }
}

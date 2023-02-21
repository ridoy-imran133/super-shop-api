using SuperShop.Entities;
using SuperShop.Helper;
using SuperShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Repository.Interface
{
    public interface IQtyTypeRepository
    {
        Task<List<QtyType>> GetAllQtyType(SuperShopDBContext pContext);
        Task<QtyType> GetQtyType(string pQtyTypeCode, SuperShopDBContext pContext);
        Task<ApiResponseModel> SaveQtyType(QtyType pbrand, SuperShopDBContext pContext);
        Task<QtyType> Delete(string pQtyTypeCode, SuperShopDBContext pContext);
    }
}

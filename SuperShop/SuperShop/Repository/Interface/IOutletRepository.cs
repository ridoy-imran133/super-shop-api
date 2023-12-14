using SuperShop.Entities;
using SuperShop.Helper;
using SuperShop.Models;
using SuperShop.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Repository.Interface
{
    public interface IOutletRepository
    {
        Task<List<Outlet>> GetAllOutlet(SuperShopDBContext pContext);
        Task<Outlet> GetOutlet(string pOutletCode, SuperShopDBContext pContext);
        Task<ApiResponseModel> SaveOutlet(Outlet poutlet, SuperShopDBContext pContext);
        Task<Outlet> Delete(string pOutletCode, SuperShopDBContext pContext);
    }
}

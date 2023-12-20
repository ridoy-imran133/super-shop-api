using SuperShop.Entities;
using SuperShop.Helper;
using SuperShop.Helper.Sequences;
using SuperShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Repository.Customer
{
    public interface ICustomerProductRepository
    {
        Task<List<Product>> GetAllProduct(string name, SuperShopDBContext pContext);
        Task<List<Product>> GetAllProductByCAT(SuperShopDBContext pContext);
        Task<List<Product>> GetCATAllProduct(SuperShopDBContext pContext);
        Task<List<CustomerMenu>> GetMenus(SuperShopDBContext pContext);

    }
    public class CustomerProductRepository : ICustomerProductRepository
    {
        public async Task<List<Product>> GetAllProduct(string name, SuperShopDBContext pContext)
        {
            return pContext.Product.Where(x => x.MenuName == name).ToList();
        }

        public async Task<List<Product>> GetAllProductByCAT(SuperShopDBContext pContext)
        {
            return pContext.Product.ToList();
        }
        public async Task<List<Product>> GetCATAllProduct(SuperShopDBContext pContext)
        {
            return pContext.Product.ToList();
        }

        public async Task<List<CustomerMenu>> GetMenus(SuperShopDBContext pContext)
        {
            return pContext.CustomerMenu.OrderBy(item=> item.Sequence).ToList();
        }

    }
}

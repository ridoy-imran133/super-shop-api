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
        Task<List<Product>> GetAllProduct(SuperShopDBContext pContext);
        Task<List<Product>> GetCATAllProduct(SuperShopDBContext pContext);
        
    }
    public class CustomerProductRepository : ICustomerProductRepository
    {
        public async Task<List<Product>> GetAllProduct(SuperShopDBContext pContext)
        {
            return pContext.Product.ToList();
        }

        public async Task<List<Product>> GetCATAllProduct(SuperShopDBContext pContext)
        {
            return pContext.Product.ToList();
        }

    }
}

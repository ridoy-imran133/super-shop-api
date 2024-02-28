using SuperShop.Entities;
using SuperShop.Helper;
using SuperShop.Helper.Sequences;
using SuperShop.Models;
using SuperShop.Models.Customer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Repository.Customer
{
    public interface ICustomerProductRepository
    {
        Task<List<CustProductDTO>> GetAllProduct(string name, SuperShopDBContext pContext);
        Task<List<Product>> GetAllProductByCAT(SuperShopDBContext pContext);
        Task<List<Product>> GetCATAllProduct(SuperShopDBContext pContext);
        Task<List<CustomerMenu>> GetMenus(SuperShopDBContext pContext);

    }
    public class CustomerProductRepository : ICustomerProductRepository
    {
        public async Task<List<CustProductDTO>> GetAllProduct(string name, SuperShopDBContext pContext)
        {
            try
            {
                //var v = pContext.Product.Where(x => x.MenuName == name).ToList();
                //var query = from product in v
                //            join image in pContext.ProductImage on product.ProductCode equals image.ProductId into CP
                //            from sg in CP.DefaultIfEmpty()
                //            where image.IsPrimary == "Y"
                //            select new CustProductDTO
                //            {
                //                ProductName = product.ProductName,
                //                SellingRate = product.SellingRate,
                //                DiscountType = product.DiscountType,
                //                DiscountAmount = product.DiscountAmount,
                //                Location = image.Location,
                //                image = image.Location != null ? Convert.ToBase64String(File.ReadAllBytes(Directory.GetCurrentDirectory() + image.Location)) : Convert.ToBase64String(File.ReadAllBytes(Directory.GetCurrentDirectory() + "/file/shared/p-1.jpg"))
                //            };
                //var resultList = query.ToList();

                List<Product> v = pContext.Product.Where(x => x.MenuName == name).ToList();

                var query = from product in v
                            join image in pContext.ProductImage on product.ProductCode equals image.ProductId into CP
                            from img in CP.DefaultIfEmpty()
                            where img != null && img.IsPrimary == "Y"
                            select new CustProductDTO
                            {
                                ProductCode = product.ProductCode,
                                ProductName = product.ProductName,
                                SellingRate = product.SellingRate,
                                DiscountType = product.DiscountType ?? "Y",
                                DiscountAmount = product.DiscountAmount ?? 0.0,
                                Location = img.Location,
                                image = img.Location != null ? Convert.ToBase64String(File.ReadAllBytes(Directory.GetCurrentDirectory() + img.Location)) : Convert.ToBase64String(File.ReadAllBytes(Directory.GetCurrentDirectory() + "/file/shared/p-1.jpg"))
                            };

                var resultList = query.ToList();

                return resultList;
            } catch(Exception e)
            {
                throw e.InnerException;
            }
            
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

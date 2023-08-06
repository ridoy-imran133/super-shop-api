using SuperShop.Entities;
using SuperShop.Helper;
using SuperShop.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Repository.Operation
{
    public interface ISalesRepository
    {
        Task<TransactionWiseProductDTO> SaleProduct(int pProductCode, SuperShopDBContext pContext);
    }
    public class SalesRepository : ISalesRepository
    {
        public async Task<TransactionWiseProductDTO> SaleProduct(int pProductCode, SuperShopDBContext pContext)
        {
            try
            {
                TransactionWiseProductDTO salesProduct = (from p in pContext.Product.Where(x => x.ProductCode == pProductCode)
                                               join s in pContext.Stock on p.ProductCode equals s.ProductCode                                               
                                               select new TransactionWiseProductDTO()
                                               {
                                                   ProductCode = p.ProductCode,
                                                   ProductName = p.ProductName,
                                                   SellingRate = p.SellingRate,
                                                   Quantity = 1,
                                                   StockAvailable = s.StockAvailable,
                                                   Amount = p.SellingRate
                                               }).FirstOrDefault();
                return salesProduct;
            }
            catch (Exception e)
            {

            }

            TransactionWiseProductDTO SaleProduct = new TransactionWiseProductDTO();
            return SaleProduct;
            //return pContext.Product.Where(x => x.ProductCode == pProductCode).FirstOrDefault();
        }
    }
}

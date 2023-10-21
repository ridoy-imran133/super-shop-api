using SuperShop.Entities;
using SuperShop.Helper;
using SuperShop.Helper.Sequences;
using SuperShop.Models;
using SuperShop.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Repository.Operation
{
    public interface ITransactionRepository
    {
        Task<ApiResponseModel> SaveTransaction(Transaction transaction, List<TransactionWiseProduct> _products, SuperShopDBContext pContext);
    }
    public class TransactionRepository : ITransactionRepository
    {
        public async Task<ApiResponseModel> SaveTransaction(Transaction transaction, List<TransactionWiseProduct> _products, SuperShopDBContext pContext)
        {
            ApiResponseModel apiResponse = new ApiResponseModel();
            using (var trans = pContext.Database.BeginTransaction())
            {
                try
                {
                    transaction.TrId = new SequenceValueGenerator(SequenceName.Transaction_seq).Next();
                    transaction.Date = DateTime.Now;
                    foreach (var product in _products)
                    {
                        Stock stock = (Stock)pContext.Stock.Where(x => x.ProductCode == product.ProductCode).FirstOrDefault();
                        stock.StockAvailable = stock.StockAvailable < 0 ? throw new Exception("Stock is not available") : stock.StockAvailable - product.Quantity;
                        pContext.Update(stock);

                        Product pro = (Product)pContext.Product.Where(x => x.ProductCode == product.ProductCode).FirstOrDefault();
                        product.TrProductId = new SequenceValueGenerator(SequenceName.TransactionWiseProduct_seq).Next();
                        product.TrId = transaction.TrId;
                        product.PurchaseRate = pro.PurchaseRate;
                        await pContext.AddAsync(product);
                        await pContext.SaveChangesAsync();
                    }
                    await pContext.AddAsync(transaction);
                    await pContext.SaveChangesAsync();
                    trans.Commit();
                    apiResponse.ResponseCode = StaticValue.SuccessCode;
                    apiResponse.ResponseMessage = "Successfull";
                }
                catch(Exception ex)
                {
                    trans.Rollback();
                    apiResponse.ResponseCode = StaticValue.Unauthorized;
                    apiResponse.ResponseMessage = ex.Message;
                }
            }
            return apiResponse;
        }
    }
}

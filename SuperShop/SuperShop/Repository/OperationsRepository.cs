using SuperShop.Entities;
using SuperShop.Helper;
using SuperShop.Models;
using SuperShop.Models.DTO;
using SuperShop.Helper.Sequences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Repository.Operation
{
    public interface IOperationsRepository
    {
        Task<TransactionWiseProductDTO> SaleProduct(int pProductCode, SuperShopDBContext pContext);
        Task<ApiResponseModel> SaveOutletWiseProducts(List<List<OutletWiseProductDTO>> products, string outletcode, SuperShopDBContext pContext);
    }
    public class OperationsRepository : IOperationsRepository
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


        public async Task<ApiResponseModel> SaveOutletWiseProducts(List<List<OutletWiseProductDTO>> products, string outletcode, SuperShopDBContext pContext)
        {
            ApiResponseModel response = new ApiResponseModel();
            using (var trans = pContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (var catWiseProducts in products)
                    {
                        foreach (var product in catWiseProducts)
                        {
                            OutletWiseProduct outletWiseProduct = pContext.OutletWiseProduct.Where(x => x.OutletCode == outletcode && x.ProductCode == product.ProductCode).FirstOrDefault();
                            if (outletWiseProduct == null)
                            {
                                if (product.IsAvailable)
                                {
                                    OutletWiseProduct outletProduct = new OutletWiseProduct();
                                    Stock stock = new Stock();
                                    StockHistory stockHistory = new StockHistory();
                                    outletProduct.Id = new SequenceValueGenerator(SequenceName.OutletWiseProduct_seq).Next();
                                    outletProduct.OutletCode = outletcode;
                                    outletProduct.CatCode = product.CatCode;
                                    outletProduct.ProductCode = product.ProductCode;

                                    stock.SId = new SequenceValueGenerator(SequenceName.Stock_seq).Next();
                                    stock.OutletCode = outletcode;
                                    stock.ProductCode = product.ProductCode;
                                    stock.StockAvailable = 0;

                                    stockHistory.SHId = new SequenceValueGenerator(SequenceName.StockHistory_seq).Next();
                                    stockHistory.OutletCode = outletcode;
                                    stockHistory.ProductCode = product.ProductCode;
                                    stockHistory.StockIn = 0;
                                    stockHistory.CreatedBy = "system";
                                    stockHistory.CreatedDate = DateTime.Now;

                                    pContext.Add(outletProduct);
                                    await pContext.SaveChangesAsync();

                                    pContext.Add(stock);
                                    await pContext.SaveChangesAsync();

                                    pContext.Add(stockHistory);
                                    await pContext.SaveChangesAsync();
                                }
                            }
                            else
                            {
                                if (!product.IsAvailable)
                                {
                                    pContext.OutletWiseProduct.Remove(outletWiseProduct);
                                    pContext.Stock.Remove(pContext.Stock.Where(x => x.OutletCode == outletcode && x.ProductCode == product.ProductCode).FirstOrDefault());
                                    StockHistory stockHistory = new StockHistory();
                                    stockHistory.SHId = new SequenceValueGenerator(SequenceName.StockHistory_seq).Next();
                                    stockHistory.OutletCode = outletcode;
                                    stockHistory.ProductCode = product.ProductCode;
                                    stockHistory.CreatedBy = "system";
                                    stockHistory.CreatedDate = DateTime.Now;
                                    stockHistory.StockIn = 0;
                                    pContext.Add(stockHistory);
                                    await pContext.SaveChangesAsync();
                                }
                            }
                        }
                    }
                    trans.Commit();
                    response.ResponseCode = StaticValue.SuccessCode;
                    response.ResponseMessage = "Successfull";
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    response.ResponseCode = StaticValue.Unauthorized;
                    response.ResponseMessage = ex.Message;
                }

            }                
            return response;
        }
    }
}

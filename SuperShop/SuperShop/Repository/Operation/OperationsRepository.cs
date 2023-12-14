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
        Task<List<List<OutletWiseProductDTO>>> GetOutletWiseProducts(string pOutletCode, SuperShopDBContext pContext);
        Task<ApiResponseModel> SaveOutletWiseProducts(List<List<OutletWiseProductDTO>> products, string outletcode, SuperShopDBContext pContext);
        Task<List<List<StockDTO>>> GetOutletWiseProductStock(string outletcode, SuperShopDBContext pContext);
        Task<ApiResponseModel> SaveOutletWiseProductsStock(List<List<StockDTO>> stocks, string outletcode, SuperShopDBContext pContext);
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

        public async Task<List<List<OutletWiseProductDTO>>> GetOutletWiseProducts(string pOutletCode, SuperShopDBContext pContext)
        {
            return ((from a in pContext.Product
                     join b in pContext.OutletWiseProduct on a.ProductCode equals b.ProductCode
                     join c in pContext.Category on b.CatCode equals c.CatCode
                     where b.OutletCode == pOutletCode
                     select new OutletWiseProductDTO
                     {
                         CatCode = a.CatCode,
                         CatName = c.CategoryName,
                         ProductCode = a.ProductCode,
                         ProductName = a.ProductName,
                         IsAvailable = true,
                     }).ToList().Concat((from d in pContext.Product.Where(a => !pContext.OutletWiseProduct.Any(b => a.ProductCode == b.ProductCode && b.OutletCode == pOutletCode))
                                         join e in pContext.Category on d.CatCode equals e.CatCode
                                         select new OutletWiseProductDTO
                                         {
                                             CatCode = d.CatCode,
                                             CatName = e.CategoryName,
                                             ProductCode = d.ProductCode,
                                             ProductName = d.ProductName,
                                             IsAvailable = false,
                                         }).ToList()).ToList()).GroupBy(u => u.CatCode)
                                            .Select(grp => grp.ToList())
                                            .ToList();
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

        public async Task<List<List<StockDTO>>> GetOutletWiseProductStock(string outletcode, SuperShopDBContext pContext)
        {
            var val = (from a in pContext.Stock
                       join b in pContext.Product on a.ProductCode equals b.ProductCode
                       join c in pContext.Category on b.CatCode equals c.CatCode
                       where a.OutletCode == outletcode
                       select new StockDTO
                       {
                           ProductCode = a.ProductCode,
                           ProductName = b.ProductName,
                           CatName = c.CategoryName,
                           OutletCode = a.OutletCode,
                           StockAvailable = a.StockAvailable,
                           StockIn = 0,
                       }).ToList().GroupBy(u => u.CatName)
                                            .Select(grp => grp.ToList())
                                            .ToList(); ;
            return val;
        }

        public async Task<ApiResponseModel> SaveOutletWiseProductsStock(List<List<StockDTO>> stocks, string outletcode, SuperShopDBContext pContext)
        {
            ApiResponseModel response = new ApiResponseModel();
            using (var trans = pContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (var catWiseStocks in stocks)
                    {
                        foreach (var stockVal in catWiseStocks)
                        {
                            if(stockVal.StockIn > 0)
                            {
                                StockHistory stockHistory = new StockHistory();

                                Stock stock = pContext.Stock.Where(x=> x.ProductCode == stockVal.ProductCode && x.OutletCode == outletcode).FirstOrDefault();
                                stock.StockAvailable = stock.StockAvailable + stockVal.StockIn;

                                stockHistory.SHId = new SequenceValueGenerator(SequenceName.StockHistory_seq).Next();
                                stockHistory.ProductCode = stockVal.ProductCode;
                                stockHistory.OutletCode = outletcode;
                                stockHistory.StockIn = stockVal.StockIn;
                                stockHistory.CreatedBy = "system";
                                stockHistory.CreatedDate = DateTime.Now;

                                pContext.Update(stock);
                                await pContext.SaveChangesAsync();

                                pContext.Add(stockHistory);
                                await pContext.SaveChangesAsync();

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

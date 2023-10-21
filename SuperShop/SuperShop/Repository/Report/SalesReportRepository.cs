using SuperShop.Entities;
using SuperShop.Helper;
using SuperShop.Helper.Sequences;
using SuperShop.Models;
using SuperShop.Models.DTO;
using SuperShop.Models.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace SuperShop.Repository.Report
{
    public interface ISalesReportRepository
    {
        Task<List<SalesReport>> ProductWiseSaleReport(ReportPerams reports, SuperShopDBContext pContext);
    }
    public class SalesReportRepository : ISalesReportRepository
    {
        public async Task<List<SalesReport>> ProductWiseSaleReport(ReportPerams reports, SuperShopDBContext pContext)
        {
            DateTime startDate = new DateTime(2023, 08, 11);
            DateTime endDate = new DateTime(2023, 08, 12);

            //SELECT CAST(a.[Date] AS DATE), b.ProductCode, c.ProductName, SUM(b.Quantity)
            //FROM[Shop].[Operation].[Transaction] a
            //INNER JOIN[Shop].[Operation].[TransactionWiseProduct] b on a.TrId = b.TrId
            //INNER JOIN[Shop].[dbo].[Product] c on b.ProductCode = c.ProductCode
            //WHERE a.[Date] BETWEEN '2023-08-11' AND '2023-08-12'
            //Group by  b.ProductCode, CAST(a.[Date] AS DATE), c.ProductName;

            var query = from a in pContext.Transaction
                        join b in pContext.TransactionWiseProduct on a.TrId equals b.TrId
                        join c in pContext.Product on b.ProductCode equals c.ProductCode
                        where a.Date >= startDate && a.Date <= endDate
                        group new { a.Date, b.ProductCode, c.ProductName, b.Quantity } by new { a.Date.Date, b.ProductCode, c.ProductName } into g
                        select new SalesReport
                        {
                            SaleDate = g.Key.Date.ToString("yyyy-MM-dd"),
                            ProductCode = g.Key.ProductCode,
                            ProductName = g.Key.ProductName,
                            Quantity = g.Sum(item => item.Quantity)
                        };
            return query.ToList();
        }
    }
}

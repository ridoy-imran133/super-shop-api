using SuperShop.Helper;
using SuperShop.Models.Report;
using SuperShop.Repository.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Services.Report
{
    public interface ISalesReportService
    {
        Task<List<SalesReport>> ProductWiseSaleReport();
    }
    public class SalesReportService : ISalesReportService
    {
        private readonly ISalesReportRepository _ISalesReportRepository;

        public SalesReportService()
        {
            _ISalesReportRepository = new SalesReportRepository();
        }

        public async Task<List<SalesReport>> ProductWiseSaleReport()
        {
            using (var _context = new SuperShopDBContext())
            {
                return await _ISalesReportRepository.ProductWiseSaleReport(new ReportPerams(),_context);
            }
        }
    }
}

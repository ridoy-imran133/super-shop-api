using SuperShop.Repository.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Services.Report
{
    public interface IInventoryReportService
    {
        //Task<ApiResponseModel> SearchProduct(int pProductCode);
    }
    public class InventoryReportService : IInventoryReportService
    {
        private readonly IInventoryReportRepository _IInventoryReportRepository;

        public InventoryReportService()
        {
            _IInventoryReportRepository = new InventoryReportRepository();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperShop.Services.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ISalesReportService _ISalesReportService;
        private readonly IInventoryReportService _IInventoryReportService;
        public ReportController(ISalesReportService salesReportService, IInventoryReportService inventoryReportService)
        {
            _ISalesReportService = salesReportService;
            _IInventoryReportService = inventoryReportService;
        }

        [HttpGet]
        [Route("SalesReport")]
        public async Task<IActionResult> SalesReport()
        {
            var products = await _ISalesReportService.ProductWiseSaleReport();
            return Ok(new
            {
                products = products
            });
        }
    }
}

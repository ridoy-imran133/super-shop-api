using Microsoft.AspNetCore.Mvc;
using SuperShop.Services.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISalesService _ISalesService;
        public SalesController(ISalesService salesService)
        {
            _ISalesService = salesService;
        }

        [HttpGet]
        [Route("saleProduct/{pcode}")]
        public async Task<IActionResult> SearchProduct(string pcode)
        {
            var product = await _ISalesService.SearchProduct(int.Parse(pcode));
            return Ok(new
            {
                product = product
            });
        }
    }
}

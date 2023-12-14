using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperShop.Services.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CProductController : ControllerBase
    {
        private readonly ICustProductService _ICustProductService;
        public CProductController(ICustProductService custProductService)
        {
            _ICustProductService = custProductService;
        }

        [HttpGet]
        [Route("getAllProduct")]
        public async Task<IActionResult> GetAllProduct()
        {
            var products = await _ICustProductService.GetAllProduct();
            return Ok(new
            {
                products = products
            });
        }

        [HttpGet]
        [Route("getAllProductTest")]
        public async Task<IActionResult> GetAllProductTest()
        {
            var products = "hi";
            return Ok(new
            {
                products = products
            });
        }
    }
}

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
        [Route("getAllProduct/{name}")]
        public async Task<IActionResult> GetAllProduct(string name)
        {
            var products = await _ICustProductService.GetAllProduct(name);
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

        [HttpGet]
        [Route("getMenus")]
        public async Task<IActionResult> GetMenus()
        {
            var menus = await _ICustProductService.GetMenus();
            return Ok(new
            {
                menus = menus
            });
        }
    }
}

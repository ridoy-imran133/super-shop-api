using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperShop.Models.DTO;
using SuperShop.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _IBrandService;
        public BrandController(IBrandService brandService)
        {
            _IBrandService = brandService;
        }

        [HttpGet]
        [Route("getAllBrand")]
        public async Task<IActionResult> GetAllBrand()
        {
            var brands = await _IBrandService.GetAllBrand();
            return Ok(new
            {
                brands = brands
            });
        }

        [HttpGet]
        [Route("getbrand/{brandcode}")]
        public async Task<IActionResult> Getbrand(string brandcode)
        {
            var brand = await _IBrandService.GetBrand(brandcode);
            return Ok(new
            {
                brand = brand
            });
        }

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> SaveorUpdate(BrandDTO brand)
        {
            var response = await _IBrandService.SaveBrand(brand);
            return Ok(new
            {
                response = response
            });
        }

        [HttpGet]
        [Route("delete")]
        public async Task<IActionResult> DeleteBrand(string brandcode)
        {
            var response = await _IBrandService.DeleteBrand(brandcode);
            return Ok(new
            {
                response = response
            });
        }
    }
}

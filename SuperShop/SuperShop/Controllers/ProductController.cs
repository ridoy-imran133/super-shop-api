using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SuperShop.Helper;
using SuperShop.Helper.utils;
using SuperShop.Models.DTO;
using SuperShop.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Controllers
{
    //[Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _IProductService;
        public ProductController(IProductService productService)
        {
            _IProductService = productService;
        }

        [HttpGet]
        [Route("getAllProduct")]
        public async Task<IActionResult> GetAllProduct()
        {
            var products = await _IProductService.GetAllProduct();
            return Ok(new
            {
                products = products
            });
        }

        [HttpGet]
        [Route("getAllProductByCat")]
        public async Task<IActionResult> getAllProductByCat()
        {
            var products = await _IProductService.GetcATAllProductDataDic();
            return Ok(new
            {
                products = products
            });
        }

        [HttpGet]
        [Route("getAllProductByCatanother")]
        public async Task<IActionResult> getAllProductByCatanother()
        {
            var products = await _IProductService.GetcATAllProduct();
            return Ok(new
            {
                products = products
            });
        }

        [HttpGet]
        [Route("getproduct/{productcode}")]
        public async Task<IActionResult> Getproduct(int productcode)
        {
            var product = await _IProductService.GetProduct(productcode);
            return Ok(new
            {
                product = product
            });
        }

        [HttpPost]
        [Route("save")]
        //public async Task<IActionResult> SaveorUpdate(ProductDTO product)
        public async Task<IActionResult> SaveorUpdate(Object obj)
        {
            ProductDTO products = ObjectValueConversion.SingleValue<ProductDTO>("ProductInfo", obj);

            List<UploadFileModel> uploadFile = ObjectValueConversion.MultiValue<UploadFileModel>("UploadFile", obj);

            var response = await _IProductService.SaveProduct(products, uploadFile);
            return Ok(new
            {
                response = response
            });
        }

        [HttpGet]
        [Route("delete")]
        public async Task<IActionResult> DeleteProduct(int productcode)
        {
            var response = await _IProductService.DeleteProduct(productcode);
            return Ok(new
            {
                response = response
            });
        }

        [HttpGet]
        [Route("categoryWiseProduct")]
        public async Task<IActionResult> GetCategoryWiseProduct()
        {
            var products = await _IProductService.GetAllProduct();
            return Ok(new
            {
                products = products
            });
        }
    }
}

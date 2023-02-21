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
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _ICategoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _ICategoryService = categoryService;
        }

        [HttpGet]
        [Route("getAllCategory")]
        public async Task<IActionResult> GetAllCategory()
        {
            var categories = await _ICategoryService.GetAllCategory();
            return Ok(new
            {
                categories = categories
            });
        }

        [HttpGet]
        [Route("getCategory/{catcode}")]
        public async Task<IActionResult> GetCategory(string catcode)
        {
            var categories = await _ICategoryService.GetCategory(catcode);
            return Ok(new
            {
                categories = categories
            });
        }

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> SaveorUpdate(CategoryDTO category)
        {
            var response = await _ICategoryService.SaveCategory(category);
            return Ok(new
            {
                response = response
            });
        }

        [HttpGet]
        [Route("delete")]
        public async Task<IActionResult> DeleteCategory(string catcode)
        {
            var response = await _ICategoryService.DeleteCategory(catcode);
            return Ok(new
            {
                response = response
            });
        }
    }
}

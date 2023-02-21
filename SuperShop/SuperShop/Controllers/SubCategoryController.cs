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
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryService _ISubCategoryService;
        public SubCategoryController(ISubCategoryService subCategoryService)
        {
            _ISubCategoryService = subCategoryService;
        }

        [HttpGet]
        [Route("GetAllSubCategory")]
        public async Task<IActionResult> GetAllSubCategory()
        {
            var subcategories = await _ISubCategoryService.GetAllSubCategory();
            return Ok(new
            {
                subcategories = subcategories
            });
        }

        [HttpGet]
        [Route("getSubCategory/{subcatcode}")]
        public async Task<IActionResult> GetSubCategory(string subcatcode)
        {
            var subcategory = await _ISubCategoryService.GetSubCategory(subcatcode);
            return Ok(new
            {
                subcategory = subcategory
            });
        }

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> SaveorUpdate(SubCategoryDTO subcategory)
        {
            var response = await _ISubCategoryService.SaveSubCategory(subcategory);
            return Ok(new
            {
                response = response
            });
        }

        [HttpGet]
        [Route("delete")]
        public async Task<IActionResult> DeleteSubCategory(string subcatcode)
        {
            var response = await _ISubCategoryService.DeleteSubCategory(subcatcode);
            return Ok(new
            {
                response = response
            });
        }
    }
}

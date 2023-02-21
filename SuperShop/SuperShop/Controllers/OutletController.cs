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
    public class OutletController : ControllerBase
    {
        private readonly IOutletService _IOutletService;
        public OutletController(IOutletService outletService)
        {
            _IOutletService = outletService;
        }

        [HttpGet]
        [Route("getAllOutlet")]
        public async Task<IActionResult> GetAllOutlet()
        {
            var outlets = await _IOutletService.GetAllOutlet();
            return Ok(new
            {
                outlets = outlets
            });
        }

        [HttpGet]
        [Route("getoutlet/{outletcode}")]
        public async Task<IActionResult> Getoutlet(string outletcode)
        {
            var outlet = await _IOutletService.GetOutlet(outletcode);
            return Ok(new
            {
                outlet = outlet
            });
        }

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> SaveorUpdate(OutletDTO outlet)
        {
            var response = await _IOutletService.SaveOutlet(outlet);
            return Ok(new
            {
                response = response
            });
        }

        [HttpGet]
        [Route("delete")]
        public async Task<IActionResult> DeleteOutlet(string outletcode)
        {
            var response = await _IOutletService.DeleteOutlet(outletcode);
            return Ok(new
            {
                response = response
            });
        }
    }
}

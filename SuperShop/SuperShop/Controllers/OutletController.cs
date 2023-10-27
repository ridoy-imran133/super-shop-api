using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        //[HttpGet]
        //[Route("getProductsByOutlet/{outletcode}")]
        //public async Task<IActionResult> GetOutletWiseProducts(string outletcode)
        //{
        //    var outletwiseProducts = await _IOutletService.GetOutletWiseProducts(outletcode);
        //    return Ok(new
        //    {
        //        outletwiseProducts = outletwiseProducts
        //    });
        //}

        //[HttpPost]
        //[Route("saveProducts")]
        //public async Task<IActionResult> saveProducts(object BusinessData)
        //{
        //    var businessData = Newtonsoft.Json.Linq.JObject.Parse(BusinessData.ToString());
        //    string objData = businessData["products"].ToString();
        //    var res = JsonConvert.DeserializeObject<List<List<OutletWiseProductDTO>>>(objData);
        //    return Ok();
        //}
    }
}

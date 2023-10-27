﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SuperShop.Models.DTO;
using SuperShop.Services.Interface;
using SuperShop.Services.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OutletProductsController : ControllerBase
    {
        private readonly IOutletService _IOutletService;
        private readonly IOperationsService _IOperationsService;
        public OutletProductsController(IOutletService outletService, IOperationsService operationsService)
        {
            _IOutletService = outletService;
            _IOperationsService = operationsService;
        }

        [HttpGet]
        [Route("getProducts/{outletcode}")]
        public async Task<IActionResult> GetOutletWiseProducts(string outletcode)
        {
            var outletwiseProducts = await _IOutletService.GetOutletWiseProducts(outletcode);
            return Ok(new
            {
                outletwiseProducts = outletwiseProducts
            });
        }
        [HttpPost]
        [Route("saveProducts")]
        public async Task<IActionResult> saveProducts(object BusinessData)
        {
            var businessData = Newtonsoft.Json.Linq.JObject.Parse(BusinessData.ToString());
            var products = JsonConvert.DeserializeObject<List<List<OutletWiseProductDTO>>>(businessData["products"].ToString());
            var outletcode = businessData["outlet"].ToString();
            var response = await _IOperationsService.SaveOutletWiseProducts(products, outletcode);
            return Ok(new
            {
                response = response
            });
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SuperShop.Entities;
using SuperShop.Models;
using SuperShop.Models.Customer;
using SuperShop.Models.DTO;
using SuperShop.Services.Customer;
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
    public class OTransactionController : ControllerBase
    {
        private readonly IOutletService _IOutletService;
        private readonly ICustTransactionService _ICustTransactionService;
        public OTransactionController(IOutletService outletService, ICustTransactionService custTransactionService)
        {
            _IOutletService = outletService;
            _ICustTransactionService = custTransactionService;
        } 

        [HttpPost]
        [Route("transaction")]
        public async Task<IActionResult> OnlineTransaction(Object data)
        {
            var businessData = Newtonsoft.Json.Linq.JObject.Parse(data.ToString());
            string objData = businessData["cartproducts"] != null ? businessData["cartproducts"].ToString() : string.Empty;
            var products = JsonConvert.DeserializeObject<List<ProductDTO>>(objData);

            string objUserInfo = businessData["userinfo"] != null ? businessData["userinfo"].ToString() : string.Empty;
            var user = JsonConvert.DeserializeObject<Customer2>(objUserInfo);

            string paymentMethod = businessData["paymentMethod"] != null ? businessData["paymentMethod"].ToString() : string.Empty;
            ApiResponseModel apiResponse = await _ICustTransactionService.SaveCustTransaction(products, user, paymentMethod);
            return Ok(new
            {
                response = apiResponse
            });
        }
    }
}

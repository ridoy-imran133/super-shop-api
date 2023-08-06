using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace SuperShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> Save(Transaction transaction)
        {
            //var response = await _IBrandService.SaveBrand(brand);
            //return Ok(new
            //{
            //    response = response
            //});
            return Ok();
        }
    }
}

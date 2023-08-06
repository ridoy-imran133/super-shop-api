using Microsoft.AspNetCore.Mvc;
using SuperShop.Models.DTO;
using SuperShop.Services.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        
        public TransactionController()
        {
            
        }

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> Save(TransactionDTO transaction)
        {
            return Ok();
        }
    }
}

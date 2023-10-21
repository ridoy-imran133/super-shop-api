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
        private readonly ITransactionService _ITransactionService;
        public TransactionController(ITransactionService transactionService)
        {
            _ITransactionService = transactionService;
        }

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> Save(TransactionDTO transaction)
        {
            //return Ok();
            var response = await _ITransactionService.SaveTransaction(transaction);
            return Ok(new
            {
                response = response
            });
        }
    }
}

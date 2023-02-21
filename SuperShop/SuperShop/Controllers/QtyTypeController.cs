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
    public class QtyTypeController : ControllerBase
    {
        private readonly IQtyTypeService _IQtyTypeService;
        public QtyTypeController(IQtyTypeService qtytypeService)
        {
            _IQtyTypeService = qtytypeService;
        }

        [HttpGet]
        [Route("getAllQtyType")]
        public async Task<IActionResult> GetAllQtyType()
        {
            var qtytypes = await _IQtyTypeService.GetAllQtyType();
            return Ok(new
            {
                qtytypes = qtytypes
            });
        }

        [HttpGet]
        [Route("getqtytype/{qtytypecode}")]
        public async Task<IActionResult> Getqtytype(string qtytypecode)
        {
            var qtytype = await _IQtyTypeService.GetQtyType(qtytypecode);
            return Ok(new
            {
                qtytype = qtytype
            });
        }

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> SaveorUpdate(QtyTypeDTO qtytype)
        {
            var response = await _IQtyTypeService.SaveQtyType(qtytype);
            return Ok(new
            {
                response = response
            });
        }

        [HttpGet]
        [Route("delete")]
        public async Task<IActionResult> DeleteQtyType(string qtytypecode)
        {
            var response = await _IQtyTypeService.DeleteQtyType(qtytypecode);
            return Ok(new
            {
                response = response
            });
        }
    }
}

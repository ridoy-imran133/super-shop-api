using AutoMapper;
using SuperShop.Helper;
using SuperShop.Models;
using SuperShop.Models.DTO;
using SuperShop.Repository.Operation;
using SuperShop.Services.Interface.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Services.Operation
{
    public interface ISalesService
    {
        Task<ApiResponseModel> SearchProduct(int pProductCode);
    }
    public class SalesService : ISalesService
    {
        private readonly ISalesRepository _ISalesRepository;
        private readonly IUploadedFileService _IUploadedFileService;
        private readonly IMapper _IMapper;

        public SalesService(IUploadedFileService uploadedFileService, IMapper iMapper)
        {
            _ISalesRepository = new SalesRepository();
            _IUploadedFileService = uploadedFileService;
            _IMapper = iMapper;
        }
        public async Task<ApiResponseModel> SearchProduct(int pProductCode)
        {
            ApiResponseModel apiResponse = new ApiResponseModel();
            try
            {
                using (var _context = new SuperShopDBContext())
                {
                    apiResponse.ResponseData = await _ISalesRepository.SaleProduct(pProductCode, _context);
                    apiResponse.ResponseCode = StaticValue.SuccessCode;
                    apiResponse.ResponseMessage = "Product Fetch Successfull";
                }
            }
            catch (Exception ex)
            {
                apiResponse.ResponseCode = StaticValue.Unauthorized;
                apiResponse.ResponseMessage = ex.Message;
            }
            return apiResponse;
        }
    }
}

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
    public interface IOperationsService
    {
        Task<ApiResponseModel> SearchProduct(int pProductCode);
        Task<List<List<OutletWiseProductDTO>>> GetOutletWiseProducts(string pOutletCode);
        Task<ApiResponseModel> SaveOutletWiseProducts(List<List<OutletWiseProductDTO>> products, string outletcode);
        Task<List<List<StockDTO>>> GetOutletWiseProductStock(string pOutletCode);
        Task<ApiResponseModel> SaveOutletWiseProductsStock(List<List<StockDTO>> stocks, string outletcode);
    }
    public class OperationsService : IOperationsService
    {
        private readonly IOperationsRepository _IOperationsRepository;
        private readonly IUploadedFileService _IUploadedFileService;
        private readonly IMapper _IMapper;

        public OperationsService(IUploadedFileService uploadedFileService, IMapper iMapper)
        {
            _IOperationsRepository = new OperationsRepository();
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
                    apiResponse.ResponseData = await _IOperationsRepository.SaleProduct(pProductCode, _context);
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

        public async Task<ApiResponseModel> SaveOutletWiseProducts(List<List<OutletWiseProductDTO>> products, string outletcode)
        {
            ApiResponseModel apiResponse = new ApiResponseModel();
            try
            {
                
                using (var _context = new SuperShopDBContext())
                {
                    apiResponse = await _IOperationsRepository.SaveOutletWiseProducts(products, outletcode, _context);
                }
            }
            catch (Exception ex)
            {

            }
            return apiResponse;
        }

        public async Task<List<List<OutletWiseProductDTO>>> GetOutletWiseProducts(string pOutletCode)
        {
            using (var _context = new SuperShopDBContext())
            {
                return await _IOperationsRepository.GetOutletWiseProducts(pOutletCode, _context);
            }
        }

        public async Task<List<List<StockDTO>>> GetOutletWiseProductStock(string pOutletCode)
        {
            using (var _context = new SuperShopDBContext())
            {
                return await _IOperationsRepository.GetOutletWiseProductStock(pOutletCode, _context);
            }
        }

        public async Task<ApiResponseModel> SaveOutletWiseProductsStock(List<List<StockDTO>> stocks, string outletcode)
        {
            ApiResponseModel apiResponse = new ApiResponseModel();
            try
            {

                using (var _context = new SuperShopDBContext())
                {
                    apiResponse = await _IOperationsRepository.SaveOutletWiseProductsStock(stocks, outletcode, _context);
                }
            }
            catch (Exception ex)
            {

            }
            return apiResponse;
        }
    }
}

using AutoMapper;
using SuperShop.Entities;
using SuperShop.Helper;
using SuperShop.Models;
using SuperShop.Models.DTO;
using SuperShop.Repository.Interface;
using SuperShop.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Services.Implementation
{
    public class OutletService : IOutletService
    {
        private readonly IOutletRepository _IOutletRepository;
        private readonly IMapper _IMapper;

        public OutletService(IOutletRepository outletRepository, IMapper iMapper)
        {
            _IOutletRepository = outletRepository;
            _IMapper = iMapper;
        }

        public async Task<List<OutletDTO>> GetAllOutlet()
        {
            using (var _context = new SuperShopDBContext())
            {
                return _IMapper.Map<List<OutletDTO>>(await _IOutletRepository.GetAllOutlet(_context));
            }
        }

        public async Task<ApiResponseModel> GetOutlet(String pOutletCode)
        {
            ApiResponseModel apiResponse = new ApiResponseModel();
            try
            {
                using (var _context = new SuperShopDBContext())
                {
                    apiResponse.ResponseData = _IMapper.Map<BrandDTO>(await _IOutletRepository.GetOutlet(pOutletCode, _context));
                    apiResponse.ResponseCode = StaticValue.SuccessCode;
                    apiResponse.ResponseMessage = "Outlet Fetch Successfull";
                }
            }
            catch (Exception ex)
            {
                apiResponse.ResponseCode = StaticValue.Unauthorized;
                apiResponse.ResponseMessage = ex.Message;
            }
            return apiResponse;
        }

        public async Task<ApiResponseModel> SaveOutlet(OutletDTO pOutlet)
        {
            ApiResponseModel apiResponse = new ApiResponseModel();
            try
            {
                using (var _context = new SuperShopDBContext())
                {
                    apiResponse = await _IOutletRepository.SaveOutlet(_IMapper.Map<Outlet>(pOutlet), _context);
                    //apiResponse.ResponseCode = responseMessage.ResponseCode;
                    //apiResponse.ResponseMessage = responseMessage.ResponseMessage;
                }
            }
            catch(Exception ex)
            {

            }
            return apiResponse;
        }

        public async Task<ApiResponseModel> DeleteOutlet(String pOutletCode)
        {
            ApiResponseModel apiResponse = new ApiResponseModel();
            try
            {
                using (var _context = new SuperShopDBContext())
                {
                    apiResponse.ResponseData = _IMapper.Map<OutletDTO>(await _IOutletRepository.Delete(pOutletCode, _context));
                    apiResponse.ResponseCode = StaticValue.SuccessCode;
                    apiResponse.ResponseMessage = "Brand Fetch Successfull";
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

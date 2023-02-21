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
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _IBrandRepository;
        private readonly IMapper _IMapper;

        public BrandService(IBrandRepository brandRepository, IMapper iMapper)
        {
            _IBrandRepository = brandRepository;
            _IMapper = iMapper;
        }

        public async Task<List<BrandDTO>> GetAllBrand()
        {
            using (var _context = new SuperShopDBContext())
            {
                return _IMapper.Map<List<BrandDTO>>(await _IBrandRepository.GetAllBrand(_context));
            }
        }

        public async Task<ApiResponseModel> GetBrand(String pBrandCode)
        {
            ApiResponseModel apiResponse = new ApiResponseModel();
            try
            {
                using (var _context = new SuperShopDBContext())
                {
                    apiResponse.ResponseData = _IMapper.Map<BrandDTO>(await _IBrandRepository.GetBrand(pBrandCode, _context));
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

        public async Task<ApiResponseModel> SaveBrand(BrandDTO pBrand)
        {
            ApiResponseModel apiResponse = new ApiResponseModel();
            try
            {
                using (var _context = new SuperShopDBContext())
                {
                    apiResponse = await _IBrandRepository.SaveBrand(_IMapper.Map<Brand>(pBrand), _context);
                    //apiResponse.ResponseCode = responseMessage.ResponseCode;
                    //apiResponse.ResponseMessage = responseMessage.ResponseMessage;
                }
            }
            catch(Exception ex)
            {

            }
            return apiResponse;
        }

        public async Task<ApiResponseModel> DeleteBrand(String pBrandCode)
        {
            ApiResponseModel apiResponse = new ApiResponseModel();
            try
            {
                using (var _context = new SuperShopDBContext())
                {
                    apiResponse.ResponseData = _IMapper.Map<BrandDTO>(await _IBrandRepository.Delete(pBrandCode, _context));
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

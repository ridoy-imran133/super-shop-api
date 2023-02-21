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
    public class SubCategoryService : ISubCategoryService
    {
        private readonly ISubCategoryRepository _ISubCategoryRepository;
        private readonly IMapper _IMapper;

        public SubCategoryService(ISubCategoryRepository subCategoryRepository, IMapper iMapper)
        {
            _ISubCategoryRepository = subCategoryRepository;
            _IMapper = iMapper;
        }

        public async Task<List<SubCategoryDTO>> GetAllSubCategory()
        {
            using (var _context = new SuperShopDBContext())
            {
                return _IMapper.Map<List<SubCategoryDTO>>(await _ISubCategoryRepository.GetAllSubCategory(_context));
            }
        }

        public async Task<ApiResponseModel> GetSubCategory(String pSubCatCode)
        {
            ApiResponseModel apiResponse = new ApiResponseModel();
            try
            {
                using (var _context = new SuperShopDBContext())
                {
                    apiResponse.ResponseData = _IMapper.Map<CategoryDTO>(await _ISubCategoryRepository.GetSubCategory(pSubCatCode, _context));
                    apiResponse.ResponseCode = StaticValue.SuccessCode;
                    apiResponse.ResponseMessage = "Menu Fetch Successfull";
                }
            }
            catch (Exception ex)
            {
                apiResponse.ResponseCode = StaticValue.Unauthorized;
                apiResponse.ResponseMessage = ex.Message;
            }
            return apiResponse;
        }

        public async Task<ApiResponseModel> SaveSubCategory(SubCategoryDTO pSubCategory)
        {
            ApiResponseModel apiResponse = new ApiResponseModel();
            try
            {
                using (var _context = new SuperShopDBContext())
                {
                    apiResponse = await _ISubCategoryRepository.SaveSubCategory(_IMapper.Map<SubCategory>(pSubCategory), _context);
                    //apiResponse.ResponseCode = responseMessage.ResponseCode;
                    //apiResponse.ResponseMessage = responseMessage.ResponseMessage;
                }
            }
            catch(Exception ex)
            {

            }
            return apiResponse;
        }

        public async Task<ApiResponseModel> DeleteSubCategory(String pSubCatCode)
        {
            ApiResponseModel apiResponse = new ApiResponseModel();
            try
            {
                using (var _context = new SuperShopDBContext())
                {
                    apiResponse.ResponseData = _IMapper.Map<SubCategoryDTO>(await _ISubCategoryRepository.DeleteData(pSubCatCode, _context));
                    apiResponse.ResponseCode = StaticValue.SuccessCode;
                    apiResponse.ResponseMessage = "Menu Fetch Successfull";
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

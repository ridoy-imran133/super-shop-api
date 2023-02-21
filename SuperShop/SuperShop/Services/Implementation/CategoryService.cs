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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _ICategoryRepository;
        private readonly IMapper _IMapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper iMapper)
        {
            _ICategoryRepository = categoryRepository;
            _IMapper = iMapper;
        }

        public async Task<List<CategoryDTO>> GetAllCategory()
        {
            using (var _context = new SuperShopDBContext())
            {
                return _IMapper.Map<List<CategoryDTO>>(await _ICategoryRepository.GetAllCategory(_context));
            }
        }

        public async Task<ApiResponseModel> GetCategory(String pCatCode)
        {
            ApiResponseModel apiResponse = new ApiResponseModel();
            try
            {
                using (var _context = new SuperShopDBContext())
                {
                    apiResponse.ResponseData = _IMapper.Map<CategoryDTO>(await _ICategoryRepository.GetCategory(pCatCode, _context));
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

        public async Task<ApiResponseModel> SaveCategory(CategoryDTO pCategory)
        {
            ApiResponseModel apiResponse = new ApiResponseModel();
            try
            {
                using (var _context = new SuperShopDBContext())
                {
                    apiResponse = await _ICategoryRepository.SaveCategory(_IMapper.Map<Category>(pCategory), _context);
                    //apiResponse.ResponseCode = responseMessage.ResponseCode;
                    //apiResponse.ResponseMessage = responseMessage.ResponseMessage;
                }
            }
            catch(Exception ex)
            {

            }
            return apiResponse;
        }

        public async Task<ApiResponseModel> DeleteCategory(String pCatCode)
        {
            ApiResponseModel apiResponse = new ApiResponseModel();
            try
            {
                using (var _context = new SuperShopDBContext())
                {
                    apiResponse.ResponseData = _IMapper.Map<CategoryDTO>(await _ICategoryRepository.DeleteData(pCatCode, _context));
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

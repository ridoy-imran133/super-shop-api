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
    public class QtyTypeService : IQtyTypeService
    {
        private readonly IQtyTypeRepository _IQtyTypeRepository;
        private readonly IMapper _IMapper;

        public QtyTypeService(IQtyTypeRepository qtytypeRepository, IMapper iMapper)
        {
            _IQtyTypeRepository = qtytypeRepository;
            _IMapper = iMapper;
        }

        public async Task<List<QtyTypeDTO>> GetAllQtyType()
        {
            using (var _context = new SuperShopDBContext())
            {
                return _IMapper.Map<List<QtyTypeDTO>>(await _IQtyTypeRepository.GetAllQtyType(_context));
            }
        }

        public async Task<ApiResponseModel> GetQtyType(String pQtyTypeCode)
        {
            ApiResponseModel apiResponse = new ApiResponseModel();
            try
            {
                using (var _context = new SuperShopDBContext())
                {
                    apiResponse.ResponseData = _IMapper.Map<QtyTypeDTO>(await _IQtyTypeRepository.GetQtyType(pQtyTypeCode, _context));
                    apiResponse.ResponseCode = StaticValue.SuccessCode;
                    apiResponse.ResponseMessage = "QtyType Fetch Successfull";
                }
            }
            catch (Exception ex)
            {
                apiResponse.ResponseCode = StaticValue.Unauthorized;
                apiResponse.ResponseMessage = ex.Message;
            }
            return apiResponse;
        }

        public async Task<ApiResponseModel> SaveQtyType(QtyTypeDTO pQtyType)
        {
            ApiResponseModel apiResponse = new ApiResponseModel();
            try
            {
                using (var _context = new SuperShopDBContext())
                {
                    apiResponse = await _IQtyTypeRepository.SaveQtyType(_IMapper.Map<QtyType>(pQtyType), _context);
                    //apiResponse.ResponseCode = responseMessage.ResponseCode;
                    //apiResponse.ResponseMessage = responseMessage.ResponseMessage;
                }
            }
            catch(Exception ex)
            {

            }
            return apiResponse;
        }

        public async Task<ApiResponseModel> DeleteQtyType(String pQtyTypeCode)
        {
            ApiResponseModel apiResponse = new ApiResponseModel();
            try
            {
                using (var _context = new SuperShopDBContext())
                {
                    apiResponse.ResponseData = _IMapper.Map<QtyTypeDTO>(await _IQtyTypeRepository.Delete(pQtyTypeCode, _context));
                    apiResponse.ResponseCode = StaticValue.SuccessCode;
                    apiResponse.ResponseMessage = "QtyType Fetch Successfull";
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

using AutoMapper;
using SuperShop.Entities;
using SuperShop.Helper;
using SuperShop.Models;
using SuperShop.Models.DTO;
using SuperShop.Repository.Interface;
using SuperShop.Services.Interface;
using SuperShop.Services.Interface.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _IProductRepository; 
        private readonly IUploadedFileService _IUploadedFileService;
        private readonly IMapper _IMapper;

        public ProductService(IProductRepository productRepository, IUploadedFileService uploadedFileService, IMapper iMapper)
        {
            _IProductRepository = productRepository;
            _IUploadedFileService = uploadedFileService;
            _IMapper = iMapper;
        }

        public async Task<List<ProductDTO>> GetAllProduct()
        {
            using (var _context = new SuperShopDBContext())
            {
                return _IMapper.Map<List<ProductDTO>>(await _IProductRepository.GetAllProduct(_context));
            }
        }

        public async Task<List<List<ProductDTO>>> GetcATAllProduct()
        {
            using (var _context = new SuperShopDBContext())
            {
                var val = _IMapper.Map<List<ProductDTO>>(await _IProductRepository.GetAllProduct(_context));
                var vendorwisecustomer = val
                                            .GroupBy(u => u.CatCode)
                                            .Select(grp => grp.ToList())
                                            .ToList();
                return vendorwisecustomer;
            }
        }

        public async Task<Dictionary<string, List<ProductDTO>>> GetcATAllProductDataDic()    //List<List<ProductDTO>>
        {
            using (var _context = new SuperShopDBContext())
            {
                var val = _IMapper.Map<List<ProductDTO>>(await _IProductRepository.GetAllProduct(_context));
                var vendorwisecustomer = val
                                            .GroupBy(u => u.CatCode)
                                            .Select(grp => grp.ToList())
                                            .ToList();

                var dataDictionary = val.GroupBy(item => item.CatCode)
            .ToDictionary(group => group.Key, group => group.Select(item => item).ToList());
                return dataDictionary;
            }
        }

        public async Task<ApiResponseModel> GetProduct(int pProductCode)
        {
            ApiResponseModel apiResponse = new ApiResponseModel();
            try
            {
                using (var _context = new SuperShopDBContext())
                {
                    apiResponse.ResponseData = _IMapper.Map<ProductDTO>(await _IProductRepository.GetProduct(pProductCode, _context));
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

        public async Task<ApiResponseModel> SaveProduct(ProductDTO pProduct, List<UploadFileModel> uploadfiles)
        {
            ApiResponseModel apiResponse = new ApiResponseModel();
            try
            {
                List<UploadFileModel> files = _IUploadedFileService.SaveUploadedFiles(uploadfiles, "");
                using (var _context = new SuperShopDBContext())
                {
                    apiResponse = await _IProductRepository.SaveProduct(_IMapper.Map<Product>(pProduct), files, _context);
                    
                    //apiResponse.ResponseCode = responseMessage.ResponseCode;
                    //apiResponse.ResponseMessage = responseMessage.ResponseMessage;
                }
            }
            catch(Exception ex)
            {

            }
            return apiResponse;
        }

        public async Task<ApiResponseModel> DeleteProduct(int pProductCode)
        {
            ApiResponseModel apiResponse = new ApiResponseModel();
            try
            {
                using (var _context = new SuperShopDBContext())
                {
                    apiResponse.ResponseData = _IMapper.Map<ProductDTO>(await _IProductRepository.Delete(pProductCode, _context));
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

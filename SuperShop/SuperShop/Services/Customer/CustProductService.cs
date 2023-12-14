using AutoMapper;
using SuperShop.Entities;
using SuperShop.Helper;
using SuperShop.Models;
using SuperShop.Models.Customer;
using SuperShop.Repository.Customer;
using SuperShop.Services.Interface.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Services.Customer
{
    public interface ICustProductService
    {
        Task<List<CustProductDTO>> GetAllProduct();
        Task<List<List<CustProductDTO>>> GetcATAllProduct();
    }
    public class CustProductService : ICustProductService
    {
        private readonly ICustomerProductRepository _ICustomerProductRepository; 
        private readonly IUploadedFileService _IUploadedFileService;
        private readonly IMapper _IMapper;

        public CustProductService(IUploadedFileService uploadedFileService, IMapper iMapper)
        {
            _ICustomerProductRepository = new CustomerProductRepository();
            _IUploadedFileService = uploadedFileService;
            _IMapper = iMapper;
        }

        public async Task<List<CustProductDTO>> GetAllProduct()
        {
            using (var _context = new SuperShopDBContext())
            {
                return _IMapper.Map<List<CustProductDTO>>(await _ICustomerProductRepository.GetAllProduct(_context));
            }
        }

        public async Task<List<List<CustProductDTO>>> GetcATAllProduct()
        {
            using (var _context = new SuperShopDBContext())
            {
                var val = _IMapper.Map<List<CustProductDTO>>(await _ICustomerProductRepository.GetAllProduct(_context));
                var vendorwisecustomer = val
                                            .GroupBy(u => u.CatCode)
                                            .Select(grp => grp.ToList())
                                            .ToList();
                return vendorwisecustomer;
            }
        }
    }
}

using AutoMapper;
using SuperShop.Entities;
using SuperShop.Helper;
using SuperShop.Models;
using SuperShop.Models.Customer;
using SuperShop.Models.DTO;
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
        Task<List<CustProductDTO>> GetAllProduct(string name);
        Task<List<List<CustProductDTO>>> GetcATAllProduct();
        Task<List<CustomerMenuDTO>> GetMenus();
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

        public async Task<List<CustProductDTO>> GetAllProduct(string name)
        {
            using (var _context = new SuperShopDBContext())
            {
                return _IMapper.Map<List<CustProductDTO>>(await _ICustomerProductRepository.GetAllProduct(name, _context));
            }
        }

        public async Task<List<CustomerMenuDTO>> GetMenus()
        {
            using (var _context = new SuperShopDBContext())
            {
                return _IMapper.Map<List<CustomerMenuDTO>>(await _ICustomerProductRepository.GetMenus(_context));
            }
        }

        public async Task<List<List<CustProductDTO>>> GetcATAllProduct()
        {
            using (var _context = new SuperShopDBContext())
            {
                var val = _IMapper.Map<List<CustProductDTO>>(await _ICustomerProductRepository.GetAllProductByCAT(_context));
                var vendorwisecustomer = val
                                            .GroupBy(u => u.CatCode)
                                            .Select(grp => grp.ToList())
                                            .ToList();
                return vendorwisecustomer;
            }
        }
    }
}

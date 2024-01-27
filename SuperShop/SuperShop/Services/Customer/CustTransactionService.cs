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
    public interface ICustTransactionService
    {
        Task<ApiResponseModel> SaveCustTransaction(List<ProductDTO> products, Customer2 customer, string paymentMethod);
    }
    public class CustTransactionService : ICustTransactionService
    {
        private readonly ICustomerTransactionRepository _ICustomerTransactionRepository;

        public CustTransactionService()
        {
            _ICustomerTransactionRepository = new CustomerTransactionRepository();
        }

        public async Task<ApiResponseModel> SaveCustTransaction(List<ProductDTO> products, Customer2 customer, string paymentMethod)
        {
            ApiResponseModel apiResponse = new ApiResponseModel();
            using (var _context = new SuperShopDBContext())
            {
                apiResponse = await _ICustomerTransactionRepository.SaveCustTransaction(products, customer, paymentMethod, _context);
            }
            return apiResponse;
        }
    }
}

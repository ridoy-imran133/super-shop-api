using AutoMapper;
using SuperShop.Entities;
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
    public interface ITransactionService
    {
        Task<ApiResponseModel> SaveTransaction(TransactionDTO transaction);
    }
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _ITransactionRepository;
        private readonly IMapper _IMapper;

        public TransactionService(IMapper iMapper)
        {
            _ITransactionRepository = new TransactionRepository();
            _IMapper = iMapper;
        }
        public async Task<ApiResponseModel> SaveTransaction(TransactionDTO transaction)
        {
            ApiResponseModel apiResponse = new ApiResponseModel();
            using (var _context = new SuperShopDBContext())
            {
                Transaction tr = _IMapper.Map<Transaction>(transaction);
                List<TransactionWiseProduct> trproducts = _IMapper.Map<List<TransactionWiseProduct>>(transaction._products);
                apiResponse = await _ITransactionRepository.SaveTransaction(tr, trproducts, _context);
            }
            return apiResponse;
        }
    }
}

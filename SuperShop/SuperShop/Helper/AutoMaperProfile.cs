using AutoMapper;
using SuperShop.Entities;
using SuperShop.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Helper
{
    public class AutoMaperProfile : Profile
    {
        public AutoMaperProfile()
        {
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();
            CreateMap<SubCategory, SubCategoryDTO>();
            CreateMap<SubCategoryDTO, SubCategory>();
            CreateMap<Brand, BrandDTO>();
            CreateMap<BrandDTO, Brand>();
            CreateMap<Outlet, OutletDTO>();
            CreateMap<OutletDTO, Outlet>();
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();
            CreateMap<QtyType, QtyTypeDTO>();
            CreateMap<QtyTypeDTO, QtyType>();
            CreateMap<Stock, StockDTO>();
            CreateMap<StockDTO, Stock>();
            CreateMap<StockHistory, StockHistoryDTO>();
            CreateMap<StockHistoryDTO, StockHistory>();
            CreateMap<Transaction, TransactionDTO>();
            CreateMap<TransactionDTO, Transaction>();
            CreateMap<TransactionWiseProduct, TransactionWiseProductDTO>();
            CreateMap<TransactionWiseProductDTO, TransactionWiseProduct>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Repository.Report
{
    public interface IInventoryReportRepository
    {
        //Task<TransactionWiseProductDTO> SaleProduct(int pProductCode, SuperShopDBContext pContext);
    }
    public class InventoryReportRepository : IInventoryReportRepository
    {
    }
}

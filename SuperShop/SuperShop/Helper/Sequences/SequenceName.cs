using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Helper.Sequences
{
    public class SequenceName
    {
        public static string Product_seq { get; } = "Product_seq";
        public static string Pr_seq { get; } = "Pr_seq";
        public static string Transaction_seq { get; } = "Transaction_seq";
        public static string TransactionWiseProduct_seq { get; } = "TransactionWiseProduct_seq";
        public static string OutletWiseProduct_seq { get; } = "OutletWiseProduct_seq";
        public static string Stock_seq { get; } = "Stock_seq";
        public static string StockHistory_seq { get; } = "StockHistory_seq";
    }
}

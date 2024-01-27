using SuperShop.Entities;
using SuperShop.Helper;
using SuperShop.Helper.Sequences;
using SuperShop.Models;
using SuperShop.Models.Customer;
using SuperShop.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Repository.Customer
{
    public interface ICustomerTransactionRepository
    {
        Task<ApiResponseModel> SaveCustTransaction(List<ProductDTO> products, Customer2 customer, string paymentMethod, SuperShopDBContext pContext);
    }
    public class CustomerTransactionRepository : ICustomerTransactionRepository
    {
        public async Task<ApiResponseModel> SaveCustTransaction(List<ProductDTO> products, Customer2 customer, string paymentMethod, SuperShopDBContext pContext)
        {
            ApiResponseModel apiResponse = new ApiResponseModel();
            using (var trans = pContext.Database.BeginTransaction())
            {
                try
                {
                    Customer2 cust = new Customer2();
                    List<CustomerOrderDetails> customerOrders = new List<CustomerOrderDetails>();
                    double totalAmount = 0.0;
                    double discountAmount = 0.0;
                    cust = pContext.Customer.Where(x => x.PhoneNumber == customer.PhoneNumber).FirstOrDefault();
                    if(cust == null)
                    {
                        cust = customer;
                        cust.CustCode = new SequenceValueGenerator(SequenceName.Customer_seq).Next();
                        await pContext.AddAsync(cust);
                    }
                    else
                    {
                        cust.UserName = customer.UserName;
                        cust.Password = customer.Password;
                        cust.FullName = customer.FullName;
                        cust.Gender = customer.Gender;
                        cust.DateOfBirth = DateTime.Now;
                        cust.District = customer.District;
                        cust.Email = customer.Email;
                    }
                    pContext.SaveChangesAsync();
                    CustomerTransaction custTransaction = new CustomerTransaction();
                    custTransaction.CustTransactionCode = new SequenceValueGenerator(SequenceName.Cust_Transaction_seq).Next();
                    custTransaction.CustCode = cust.CustCode;
                    custTransaction.PaymentMethod = paymentMethod;
                    custTransaction.IsTransactionComplete = "N";
                    custTransaction.ShipmentCharge = 40;
                    //custTransaction.CouponCode = "" when have an coupon code

                    foreach (var product in products)
                    {
                        CustomerOrderDetails order = new CustomerOrderDetails();
                        Product p = pContext.Product.Where(X => X.ProductCode == product.ProductCode).FirstOrDefault();
                        order.Id = new SequenceValueGenerator(SequenceName.Customer_order_seq).Next();
                        order.CustTransactionCode = custTransaction.CustTransactionCode;
                        order.ProductCode = p.ProductCode;
                        order.Price = p.SellingRate;
                        order.Quantity = product.productQuantity;
                        order.DiscountType = p.DiscountType;
                        order.DiscountAmount = p.DiscountAmount ?? 0;
                        double pAmount = p.SellingRate * product.productQuantity;
                        totalAmount = totalAmount + pAmount;
                        discountAmount = discountAmount + order.DiscountAmount;

                        //need to update stock table
                        Stock stock = (Stock)pContext.Stock.Where(x => x.ProductCode == product.ProductCode).FirstOrDefault();
                        stock.StockAvailable = stock.StockAvailable - (decimal)product.productQuantity;
                        pContext.Update(stock);
                        customerOrders.Add(order);
                    }
              
                    custTransaction.TotalAmount = totalAmount;
                    custTransaction.DiscountAmount = discountAmount;
                    await pContext.AddAsync(custTransaction);
                    //await pContext.SaveChangesAsync();
                    await pContext.AddRangeAsync(customerOrders);
                    await pContext.SaveChangesAsync();              
                    trans.Commit();
                    apiResponse.ResponseCode = StaticValue.SuccessCode;
                    apiResponse.ResponseMessage = "Successfull";
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    apiResponse.ResponseCode = StaticValue.Unauthorized;
                    apiResponse.ResponseMessage = ex.Message;
                }
            }
            return apiResponse;
        }
    }
}

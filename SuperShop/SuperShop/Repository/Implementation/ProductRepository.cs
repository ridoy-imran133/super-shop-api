using SuperShop.Entities;
using SuperShop.Helper;
using SuperShop.Helper.Sequences;
using SuperShop.Models;
using SuperShop.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Repository.Implementation
{
    public class ProductRepository : IProductRepository
    {
        public async Task<List<Product>> GetAllProduct(SuperShopDBContext pContext)
        {
            return pContext.Product.ToList();
        }

        public async Task<List<Product>> GetCATAllProduct(SuperShopDBContext pContext)
        {
            return pContext.Product.ToList();
        }

        public async Task<Product> GetProduct(int pProductCode, SuperShopDBContext pContext)
        {
            return pContext.Product.Where(x => x.ProductCode == pProductCode).FirstOrDefault();
        }

        public async Task<ApiResponseModel> SaveProduct(Product pproduct, List<UploadFileModel> uploadfiles, SuperShopDBContext pContext)
        {
            ApiResponseModel response = new ApiResponseModel();
            bool saveProductImages = false;
            try
            {
                Product product = pContext.Product.Where(x => x.ProductCode == pproduct.ProductCode).FirstOrDefault();
                if (product == null)
                {
                    pproduct.ProductCode = new SequenceValueGenerator(SequenceName.Product_seq).Next();
                    pproduct.IsActive = "Y";
                    pproduct.IsDelete = "N";
                    pproduct.CreatedBy = "system";
                    pproduct.CreatedDate = DateTime.Now;

                    await pContext.AddAsync(pproduct);
                    await pContext.SaveChangesAsync();
                    response.ResponseCode = StaticValue.SuccessCode;
                    response.ResponseMessage = "Saved Successfully";
                    response.ResponseData = pproduct;
                    saveProductImages = await SaveProductImage(uploadfiles, pproduct.ProductCode, pContext);
                }
                else
                {
                    product.ProductName = pproduct.ProductName;
                    product.CatCode = pproduct.CatCode;
                    product.SubCatCode = pproduct.SubCatCode;
                    product.BrandCode = pproduct.BrandCode;
                    //product.ItemCode = pproduct.ItemCode;
                    product.QtyTypeCode = pproduct.QtyTypeCode;
                    product.PurchaseRate = pproduct.PurchaseRate;
                    product.SellingRate = pproduct.SellingRate;

                    product.DiscountAmount = pproduct.DiscountAmount;
                    product.DiscountType = pproduct.DiscountType;
                    product.MenuName = pproduct.MenuName;

                    product.ModifiedBy = "system";
                    product.ModifiedDate = DateTime.Now;

                    pContext.Update(product);
                    await pContext.SaveChangesAsync();
                    response.ResponseData = product;
                    response.ResponseCode = StaticValue.SuccessCode;
                    response.ResponseMessage = "Updated Successfully";
                    saveProductImages = await SaveProductImage(uploadfiles, product.ProductCode, pContext);
                }

            }
            catch (Exception ex)
            {
                response.ResponseCode = StaticValue.Unauthorized;
                response.ResponseMessage = ex.Message;
            }
            return response;
        }

        public async Task<Product> Delete(int pProductCode, SuperShopDBContext pContext)
        {
            var val = pContext.Product.Where(x => x.ProductCode == pProductCode).FirstOrDefault();
            pContext.Product.Remove(val);
            await pContext.SaveChangesAsync();
            return val;
        }

        //public async Task<Product> Delete(int pProductCode, SuperShopDBContext pContext)
        //{
        //    var commonData = from t1 in pContext.Product
        //                     join t2 in pContext.Out on t1.ID equals t2.ID
        //                     select t1;
        //    pContext.Product.Remove(val);
        //    await pContext.SaveChangesAsync();
        //    return val;
        //}

        public async Task<bool> SaveProductImage(List<UploadFileModel> uploadfiles, int productid, SuperShopDBContext pContext)
        {
            bool retvalue = true;
            ProductImage pi = new ProductImage();
            try
            {
                var imagesList = pContext.ProductImage.Where(x => x.ProductId == productid).ToList();
                pContext.ProductImage.RemoveRange(imagesList);
                pContext.SaveChanges();
                foreach (UploadFileModel fi in uploadfiles)
                {
                    pi = new ProductImage();
                    pi.Location = fi.FileLocation;
                    pi.ProductId = productid;
                    pi.IsActive = "Y";
                    pi.IsDelete = "N";
                    pi.UploadDate = DateTime.Now;
                    pi.IsPrimary = fi.IsPrimary ? "Y" : "N";

                    await pContext.AddAsync(pi);
                    await pContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                retvalue = false;
            }
            return retvalue;
        }
    }
}

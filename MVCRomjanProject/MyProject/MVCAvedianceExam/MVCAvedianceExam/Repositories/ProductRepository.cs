using MVCAvedianceExam.Models;
using MVCAvedianceExam.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCAvedianceExam.Repositories
{
    public class ProductRepository
    {
        EcommerceDBEntities dbObj = new EcommerceDBEntities();
        public IEnumerable<ProductViewModel> GetAllProduct()
        {
            List<ProductViewModel> productList = dbObj.Products.Select(e => new ProductViewModel
            {
                ProductId = e.ProductId,
                ProductName = e.ProductName,
                PurchaseDate = e.PurchaseDate,
                SupplierId = e.SupplierId,
                Email = e.Email,
                Quantity = e.Quantity,
                UnitPrice = e.UnitPrice,
                ImageName = e.ImageName,
                ImageUrl = e.ImageUrl,
                SupplierName = e.Supplier.SupplierName,
                PageTitle = "Product List"
            }).ToList();
            return productList;
        }
        public void SaveProduct(Product obj)
        {
            dbObj.Products.Add(obj);
            dbObj.SaveChanges();
        }
        public Product GetProductById(int id)
        {
            Product obj = dbObj.Products.SingleOrDefault(e => e.ProductId == id);
            return obj;
        }
        public void UpdateProduct(Product updateObj)
        {
            dbObj.Entry(updateObj).State = EntityState.Modified;
            dbObj.SaveChanges();
        }
        public void DeleteProduct(int id)
        {
            Product deleteProduct = GetProductById(id);
            dbObj.Products.Remove(deleteProduct);
            dbObj.SaveChanges();
        }
        public List<Supplier> GetSuppliers()
        {
            List<Supplier> list = dbObj.Suppliers.ToList();
            return list;
        }
        public void SaveSupplier(Supplier obj)
        {
            dbObj.Suppliers.Add(obj);
            dbObj.SaveChanges();
        }
    }
}
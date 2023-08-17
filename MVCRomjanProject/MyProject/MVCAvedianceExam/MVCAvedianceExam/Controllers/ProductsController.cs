using MVCAvedianceExam.Models;
using MVCAvedianceExam.Models.ViewModels;
using MVCAvedianceExam.Repositories;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCAvedianceExam.Controllers
{
    public class ProductsController : Controller
    {
        ProductRepository repo = new ProductRepository();
        [HttpGet]
        public ActionResult Index(string SearchString, string CurrentFilter, string SortOrder, int? page)
        {
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = CurrentFilter;
            }
            ViewBag.SortNameParam = string.IsNullOrEmpty(SortOrder) ? "name_desc" : "";
            ViewBag.CurrentFilter = SearchString;
            IEnumerable<ProductViewModel> list = repo.GetAllProduct();
            if (!string.IsNullOrEmpty(SearchString))
            {
                list = list.Where(e => e.ProductName.ToUpper().Contains(SearchString.ToUpper())).ToList();
            }
            switch (SortOrder)
            {
                case "name_desc":
                    list = list.OrderByDescending(e => e.ProductName).ToList();
                    break;
                default:
                    list = list.OrderBy(e => e.ProductName).ToList();
                    break;

            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ViewResult Create()
        {
            List<Supplier> list = repo.GetSuppliers();
            ViewBag.Suppliers = list;
            return View();
        }
        [HttpGet]
        public ViewResult Edit(int id)
        {
            List<Supplier> list = repo.GetSuppliers();
            ViewBag.Suppliers = list;
            Product productObj = repo.GetProductById(id);
            ProductViewModel obj = new ProductViewModel();
            obj.ProductId = productObj.ProductId;
            obj.ProductName = productObj.ProductName;
            obj.PurchaseDate = productObj.PurchaseDate;
            obj.SupplierId = productObj.SupplierId;
            obj.Email = productObj.Email;
            obj.Quantity = productObj.Quantity;
            obj.UnitPrice = productObj.UnitPrice;
            obj.ImageName = productObj.ImageName;
            obj.ImageUrl = productObj.ImageUrl;
            return View(obj);
        }
        [HttpPost]
        [ActionName("Create")]
        public ActionResult PostCreate(ProductViewModel obj)
        {
            var result = false;
            Product obj1;
            if (obj.ProductId == 0)
            {
                if (ModelState.IsValid)
                {
                    obj1 = new Product();
                    obj1.ProductName = obj.ProductName;
                    obj1.PurchaseDate = obj.PurchaseDate;
                    obj1.SupplierId = obj.SupplierId;
                    obj1.Email = obj.Email;
                    obj1.Quantity = obj.Quantity;
                    obj1.UnitPrice = obj.UnitPrice;
                    string fileName = Path.GetFileNameWithoutExtension(obj.ImageFile.FileName);
                    string extension = Path.GetExtension(obj.ImageFile.FileName);
                    obj1.ImageName = fileName + extension;
                    obj1.ImageUrl = "~/Images/" + obj1.ImageName;
                    fileName = Path.Combine(Server.MapPath(obj1.ImageUrl));
                    obj.ImageFile.SaveAs(fileName);
                    repo.SaveProduct(obj1);
                    result = true;

                }
            }
            else
            {
                obj1 = repo.GetProductById(obj.ProductId);
                obj1.ProductName = obj.ProductName;
                obj1.PurchaseDate = obj.PurchaseDate;
                obj1.SupplierId = obj.SupplierId;
                obj1.Email = obj.Email;
                obj1.Quantity = obj.Quantity;
                obj1.UnitPrice = obj.UnitPrice;
                if (obj.ImageFile != null)
                {
                    DeleteExistingImage(obj1.ImageName);
                    string fileName = Path.GetFileNameWithoutExtension(obj.ImageFile.FileName);
                    string extension = Path.GetExtension(obj.ImageFile.FileName);
                    obj1.ImageName = fileName + extension;
                    obj1.ImageUrl = "~/Images/" + obj1.ImageName;
                    fileName = Path.Combine(Server.MapPath(obj1.ImageUrl));
                    obj.ImageFile.SaveAs(fileName);
                }
                else
                {
                    obj.ImageName = obj1.ImageName;
                    obj.ImageUrl = obj1.ImageUrl;
                }
                repo.UpdateProduct(obj1);
                result = true;
            }
            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                List<Supplier> list = repo.GetSuppliers();
                ViewBag.Suppliers = list;
                return View();
            }
        }


        private void DeleteExistingImage(string imagename)
        {
            string root = Server.MapPath("~");
            string parent = Path.GetDirectoryName(root);
            string path = parent + "/Images/" + imagename;
            FileInfo fileObj = new FileInfo(path);
            if (fileObj.Exists)
            {
                fileObj.Delete();
            }
        }
        [HttpGet]
        public ViewResult Delete(int id)
        {
            List<Supplier> list = repo.GetSuppliers();
            ViewBag.Suppliers = list;
            Product product = repo.GetProductById(id);
            ProductViewModel obj = new ProductViewModel();
            obj.ProductId = product.ProductId;
            obj.ProductName = product.ProductName;
            obj.PurchaseDate = product.PurchaseDate;
            obj.SupplierId = product.SupplierId;
            obj.SupplierName = product.Supplier.SupplierName;
            obj.Email = product.Email;
            obj.Quantity = product.Quantity;
            obj.UnitPrice = product.UnitPrice;
            obj.ImageName = product.ImageName;
            obj.ImageUrl = product.ImageUrl;
            return View(obj);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult PostDelete(ProductViewModel viewObj)
        {
            Product obj = repo.GetProductById(viewObj.ProductId);
            string imageName = viewObj.ImageName;
            DeleteExistingImage(imageName);
            repo.DeleteProduct(obj.ProductId);
            return RedirectToAction("Index");
        }
        public PartialViewResult Details(int id)
        {
            List<Supplier> list = repo.GetSuppliers();
            ViewBag.Suppliers = list;
            Product product = repo.GetProductById(id);
            ProductViewModel obj = new ProductViewModel();
            obj.ProductId = product.ProductId;
            obj.ProductName = product.ProductName;
            obj.PurchaseDate = product.PurchaseDate;
            obj.SupplierId = product.SupplierId;
            obj.SupplierName = product.Supplier.SupplierName;
            obj.Email = product.Email;
            obj.Quantity = product.Quantity;
            obj.UnitPrice = product.UnitPrice;
            obj.ImageName = product.ImageName;
            obj.ImageUrl = product.ImageUrl;
            ViewBag.Details = "Show";
            return PartialView("_DetailsRecord", obj);
        }
    }
}
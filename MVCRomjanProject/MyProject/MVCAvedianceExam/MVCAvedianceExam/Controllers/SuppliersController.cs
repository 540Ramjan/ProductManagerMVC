using MVCAvedianceExam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCAvedianceExam.Controllers
{
    public class SuppliersController : Controller
    {
        EcommerceDBEntities dbObj = new EcommerceDBEntities();
        // GET: Suppliers
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateSupplier()
        {
            var Model = dbObj.Suppliers.ToList();
            return View(Model);
        }
        public ActionResult AjaxCreateSupplier(Supplier obj)
        {
            if (ModelState.IsValid == true)
            {
                System.Threading.Thread.Sleep(1000);
                dbObj.Suppliers.Add(obj);
                dbObj.SaveChanges();
            }
            IEnumerable<Supplier> Modal = dbObj.Suppliers.ToList();
            return PartialView("_SupplierDetails", Modal);
        }
    }
}
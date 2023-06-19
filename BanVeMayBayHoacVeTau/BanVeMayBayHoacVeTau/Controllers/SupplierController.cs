using BanVeMayBayHoacVeTau.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanVeMayBayHoacVeTau.Controllers
{
    public class SupplierController : BaseController
    {
        // GET: Supplier
        HeThongBanVeTauHoacMayBayDataContext dbContext = new HeThongBanVeTauHoacMayBayDataContext();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListSuppliers()
        {
            var check = dbContext.Customers.Where(m => m.CustomerID == Session["User"]).FirstOrDefault();
            if (check != null)
            {
                return Content("<script language='javascript' type='text/javascript'>alert('Chưa có quyền!');window.location = '/Product/ListProducts'</script>");
            }
            List<Supplier> e = dbContext.Suppliers.Select(s => s).ToList();
            return View(e);
        }
        public ActionResult Create()
        {
            var check = dbContext.Customers.Where(m => m.CustomerID == Session["User"]).FirstOrDefault();
            if (check != null)
            {
                return Content("<script language='javascript' type='text/javascript'>alert('Chưa có quyền!');window.location = '/Product/ListProducts'</script>");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Create(Supplier e, FormCollection collection)
        {
            
                try
                {
                    dbContext.Suppliers.InsertOnSubmit(e);
                    dbContext.SubmitChanges();
                    return RedirectToAction("ListSuppliers", "Supplier");
                }
                catch
                {
                    return View();
                }
        }
        public ActionResult Delete(int id)
        {
            var check = dbContext.Customers.Where(m => m.CustomerID == Session["User"]).FirstOrDefault();
            if (check != null)
            {
                return Content("<script language='javascript' type='text/javascript'>alert('Chưa có quyền!');window.location = '/Product/ListProducts'</script>");
            }
            Supplier p = dbContext.Suppliers.FirstOrDefault(s => s.SupplierID == id);
            return View(p);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var p = dbContext.Suppliers.FirstOrDefault(s => s.SupplierID == id);
                dbContext.Suppliers.DeleteOnSubmit(p);
                dbContext.SubmitChanges();

                return RedirectToAction("ListSuppliers", "Supplier");

            }
            catch
            {
                return View();
            }
        }
        public ActionResult Details(int id)
        {
            var check = dbContext.Customers.Where(m => m.CustomerID == Session["User"]).FirstOrDefault();
            if (check != null)
            {
                return Content("<script language='javascript' type='text/javascript'>alert('Chưa có quyền!');window.location = '/Product/ListProducts'</script>");
            }
            Supplier p = dbContext.Suppliers.FirstOrDefault(s => s.SupplierID == id);
            return View(p);
        }
        public ActionResult Edit(int id)
        {
            var check = dbContext.Customers.Where(m => m.CustomerID == Session["User"]).FirstOrDefault();
            if (check != null)
            {
                return Content("<script language='javascript' type='text/javascript'>alert('Chưa có quyền!');window.location = '/Product/ListProducts'</script>");
            }
            Supplier p = dbContext.Suppliers.FirstOrDefault(s => s.SupplierID == id);

            return View(p);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            try
            {
                var p = dbContext.Suppliers.Where(s => s.SupplierID == id).FirstOrDefault();
                UpdateModel(p);
                dbContext.SubmitChanges();

                return RedirectToAction("ListSuppliers", "Supplier");

            }
            catch
            {
                return RedirectToAction("Edit");
            }
        }
    }
}

using BanVeMayBayHoacVeTau.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanVeMayBayHoacVeTau.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Product
        HeThongBanVeTauHoacMayBayDataContext dbContext = new HeThongBanVeTauHoacMayBayDataContext();
        List<Product> products = null;
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListProducts(string SearchString = "")
        {

            products = null;
            if (SearchString != "")
            {
                products = dbContext.Products.Where(s => s.ProductName.ToUpper().Contains(SearchString.ToUpper())).ToList();
                //return View(products);
            }
            else
            {
                products = dbContext.Products.Select(s => s).ToList();
                //return View(products);
            }
            return View(products);
        }
        public ActionResult Details(int id)
        {
            Product p = dbContext.Products.FirstOrDefault(s => s.ProductID == id);
            return View(p);
        }
        public ActionResult Create()
        {
            var check = dbContext.Customers.Where(m => m.CustomerID == Session["User"]).FirstOrDefault();
            if (check != null)
            {
                return Content("<script language='javascript' type='text/javascript'>alert('Chưa có quyền!');window.location = '/Product/ListProducts'</script>");
            }
            ViewData["SP"] = new SelectList(dbContext.Suppliers, "SupplierID", "SupplierName");
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product p, FormCollection collection)
        {
            try
            {
                dbContext.Products.InsertOnSubmit(p);
                p.SupplierID = int.Parse(collection["SP"]);
                dbContext.SubmitChanges();
                return RedirectToAction("ListProducts");
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
            Product p = dbContext.Products.FirstOrDefault(s => s.ProductID == id);
            return View(p);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var p = dbContext.Products.FirstOrDefault(s => s.ProductID == id);
                dbContext.Products.DeleteOnSubmit(p);
                dbContext.SubmitChanges();

                return RedirectToAction("ListProducts");

            }
            catch
            {
                return View();
            }
        }
        public ActionResult Edit(int id)
        {
            var check = dbContext.Customers.Where(m => m.CustomerID == Session["User"]).FirstOrDefault();
            if (check != null)
            {
                return Content("<script language='javascript' type='text/javascript'>alert('Chưa có quyền!');window.location = '/Product/ListProducts'</script>");
            }
            Product p = dbContext.Products.FirstOrDefault(s => s.ProductID == id);
            //ViewData["SP"] = new SelectList(dbContext.Suppliers, "SupplierID", "SupplierName");
            return View(p);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            try
            {
                var p = dbContext.Products.Where(s => s.ProductID == id).FirstOrDefault();
                //p.SupplierID = int.Parse(form["SP"]);
                UpdateModel(p);
                dbContext.SubmitChanges();

                return RedirectToAction("ListProducts");

            }
            catch
            {
                return RedirectToAction("Edit");
            }
        }
    }
}
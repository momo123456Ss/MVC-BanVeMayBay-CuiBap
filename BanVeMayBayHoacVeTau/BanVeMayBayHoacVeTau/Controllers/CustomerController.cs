using BanVeMayBayHoacVeTau.Library;
using BanVeMayBayHoacVeTau.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanVeMayBayHoacVeTau.Controllers
{
    public class CustomerController : BaseController
    {
        // GET: Customer
        HeThongBanVeTauHoacMayBayDataContext dbContext = new HeThongBanVeTauHoacMayBayDataContext();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details(string id)
        {
            var check = dbContext.Customers.Where(m => m.CustomerID == Session["User"]).FirstOrDefault();
            if (check != null)
            {
                return Content("<script language='javascript' type='text/javascript'>alert('Chưa có quyền!');window.location = '/Product/ListProducts'</script>");
            }
            Customer p = dbContext.Customers.FirstOrDefault(s => s.CustomerID == id);
            return View(p);
        }
        public ActionResult ListCustomers()
        {
            var check = dbContext.Customers.Where(m => m.CustomerID == Session["User"]).FirstOrDefault();
            if (check != null)
            {
                return Content("<script language='javascript' type='text/javascript'>alert('Chưa có quyền!');window.location = '/Product/ListProducts'</script>");
            }
            List<Customer> products = dbContext.Customers.Select(s => s).ToList();
            return View(products);
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
        public ActionResult Create(Customer p, FormCollection collection)
        {
            string strerror = "";
            string customerID = collection["CustomerID"];
            string customerPhone = collection["Phone"];
            var rowuserID = dbContext.Customers.Where(m => m.CustomerID == customerID).FirstOrDefault();
            var rowuserPhone = dbContext.Customers.Where(m => m.Phone == customerPhone).FirstOrDefault();
            if (rowuserID != null)
            {
                strerror = "Tồn tại ID dùng ID khác";
            }
            else if (rowuserPhone != null)
            {
                strerror = "Tồn tại Phone dùng Phone khác";
            }
            else
            {
                try
                {
                    p.CustomerID = MyString.ToMD5(collection["CustomerID"]);
                    p.Password = MyString.ToMD5(collection["Password"]);
                    dbContext.Customers.InsertOnSubmit(p);
                    dbContext.SubmitChanges();
                    return RedirectToAction("ListCustomers");
                }
                catch
                {
                    return View();
                }
            }
            ViewBag.Error = "<span class='text-danger'>" + strerror + "</span>";
            return View();
        }
        public ActionResult Delete(string id)
        {
            var check = dbContext.Customers.Where(m => m.CustomerID == Session["User"]).FirstOrDefault();
            if (check != null)
            {
                return Content("<script language='javascript' type='text/javascript'>alert('Chưa có quyền!');window.location = '/Product/ListProducts'</script>");
            }
            Customer p = dbContext.Customers.FirstOrDefault(s => s.CustomerID == id);
            return View(p);
        }
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                var p = dbContext.Customers.FirstOrDefault(s => s.CustomerID == id);
                dbContext.Customers.DeleteOnSubmit(p);
                dbContext.SubmitChanges();

                return RedirectToAction("ListCustomers");

            }
            catch
            {
                return View();
            }
        }
        public ActionResult Edit(string id)
        {
            var check = dbContext.Customers.Where(m => m.CustomerID == Session["User"]).FirstOrDefault();
            if (check != null)
            {
                return Content("<script language='javascript' type='text/javascript'>alert('Chưa có quyền!');window.location = '/Product/ListProducts'</script>");
            }
            Customer p = dbContext.Customers.FirstOrDefault(s => s.CustomerID == id);

            return View(p);
        }
        [HttpPost]
        public ActionResult Edit(string id, FormCollection form)
        {
            try
            {
                var p = dbContext.Customers.Where(s => s.CustomerID == id).FirstOrDefault();
                p.Password = MyString.ToMD5(form["Password"]);
                UpdateModel(p);
                dbContext.SubmitChanges();

                return RedirectToAction("ListCustomers");

            }
            catch
            {
                return RedirectToAction("Edit");
            }
        }
    }
}
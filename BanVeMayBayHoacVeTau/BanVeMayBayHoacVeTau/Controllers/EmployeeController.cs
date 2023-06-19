using BanVeMayBayHoacVeTau.Library;
using BanVeMayBayHoacVeTau.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanVeMayBayHoacVeTau.Controllers
{
    public class EmployeeController : BaseController
    {
        // GET: Employee
        HeThongBanVeTauHoacMayBayDataContext dbContext = new HeThongBanVeTauHoacMayBayDataContext();
        public ActionResult Index()
        {
            var check = dbContext.Customers.Where(m => m.CustomerID == Session["User"]).FirstOrDefault();
            if (check != null)
            {
                return Content("<script language='javascript' type='text/javascript'>alert('Chưa có quyền!');window.location = '/Product/ListProducts'</script>");
            }
            return View();
        }
        public ActionResult ListEmployees()
        {
            var check = dbContext.Customers.Where(m => m.CustomerID == Session["User"]).FirstOrDefault();
            if (check != null)
            {
                return Content("<script language='javascript' type='text/javascript'>alert('Chưa có quyền!');window.location = '/Product/ListProducts'</script>");
            }
            List<Employee> e = dbContext.Employees.Select(s => s).ToList();
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
        public ActionResult Create(Employee e, FormCollection collection)
        {
            string strerror = "";
            string employeeID = MyString.ToMD5(collection["EmployeeID"]);
            var rowuser = dbContext.Employees.Where(m => m.EmployeeID == employeeID).FirstOrDefault();
            if (rowuser != null)
            {
                strerror = "Tồn tại ID dùng ID khác";
            }
            else
            {
                try
                {
                    e.EmployeeID = MyString.ToMD5(collection["EmployeeID"]);
                    e.EmployeePassword = MyString.ToMD5(collection["EmployeePassword"]);
                    dbContext.Employees.InsertOnSubmit(e);
                    dbContext.SubmitChanges();
                    return RedirectToAction("ListEmployees","Employee");
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
            Employee p = dbContext.Employees.FirstOrDefault(s => s.EmployeeID== id);
            return View(p);
        }
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                var p = dbContext.Employees.FirstOrDefault(s => s.EmployeeID == id);
                dbContext.Employees.DeleteOnSubmit(p);
                dbContext.SubmitChanges();

                return RedirectToAction("ListEmployees","Employee");

            }
            catch
            {
                return View();
            }
        }
        public ActionResult Details(string id)
        {
            var check = dbContext.Customers.Where(m => m.CustomerID == Session["User"]).FirstOrDefault();
            if (check != null)
            {
                return Content("<script language='javascript' type='text/javascript'>alert('Chưa có quyền!');window.location = '/Product/ListProducts'</script>");
            }
            Employee p = dbContext.Employees.FirstOrDefault(s => s.EmployeeID == id);
            return View(p);
        }
        public ActionResult Edit(string id)
        {
            var check = dbContext.Customers.Where(m => m.CustomerID == Session["User"]).FirstOrDefault();
            if (check != null)
            {
                return Content("<script language='javascript' type='text/javascript'>alert('Chưa có quyền!');window.location = '/Product/ListProducts'</script>");
            }
            Employee p = dbContext.Employees.FirstOrDefault(s => s.EmployeeID == id);

            return View(p);
        }
        [HttpPost]
        public ActionResult Edit(string id, FormCollection form)
        {
            try
            {
                var p = dbContext.Employees.Where(s => s.EmployeeID == id).FirstOrDefault();
                UpdateModel(p);
                dbContext.SubmitChanges();

                return RedirectToAction("ListEmployees","Employee");

            }
            catch
            {
                return RedirectToAction("Edit");
            }
        }
    }
}
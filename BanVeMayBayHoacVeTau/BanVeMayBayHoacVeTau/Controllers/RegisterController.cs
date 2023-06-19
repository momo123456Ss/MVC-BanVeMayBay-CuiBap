using BanVeMayBayHoacVeTau.Library;
using BanVeMayBayHoacVeTau.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanVeMayBayHoacVeTau.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        HeThongBanVeTauHoacMayBayDataContext dbContext = new HeThongBanVeTauHoacMayBayDataContext();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateWithCustomer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateWithCustomer(Customer p, FormCollection collection)
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
                strerror = "Tồn tại Phone";
            }
            else
            {
                try
                {
                    p.CustomerID = collection["CustomerID"];
                    p.Password = MyString.ToMD5(collection["Password"]);
                    dbContext.Customers.InsertOnSubmit(p);
                    dbContext.SubmitChanges();
                    return Content("<script language='javascript' type='text/javascript'>alert('Đã đăng ký!');window.location = '/Auth/Login'</script>");
                }
                catch
                {
                    return View();
                }
            }
            ViewBag.Error = "<span class='text-danger'>" + strerror + "</span>";
            return View();
        }
    }
}
using BanVeMayBayHoacVeTau.Library;
using BanVeMayBayHoacVeTau.Models;
using CaptchaMvc.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BanVeMayBayHoacVeTau.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        HeThongBanVeTauHoacMayBayDataContext dbContext = new HeThongBanVeTauHoacMayBayDataContext();

        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (!Session["User"].Equals(""))
            {
                return RedirectToAction("ListProducts", "Product");
            }
            ViewBag.Error = "";
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(FormCollection field,string returnUrl)
        {
            //MyString.userTMP = field["username"];
            bool isCaptchaVaild = ValidateCaptcha(Request["g-recaptcha-response"]);
            string strerror = "";
            string username = field["username"];
            string password = MyString.ToMD5(field["password"]);
            var rowuser = dbContext.Employees.Where(m => m.EmployeeID == username).FirstOrDefault();
            var rowCus = dbContext.Customers.Where(m => m.CustomerID == username).FirstOrDefault();
            if (ModelState.IsValid)
            {
                if (isCaptchaVaild)
                {
                    if (rowCus != null)
                    {
                        if (rowCus.Password.Equals(password))
                        {

                            Session["User"] = rowCus.CustomerID;
                            Session["Name"] = rowCus.FirstName + " " + rowCus.LastName;
                            return RedirectToAction("ListProducts", "Product");
                        }
                        else
                        {
                            strerror = "Mật khẩu ko đúng " + password;
                        }
                    }
                    else if (rowuser != null)
                    {
                        if (rowuser.EmployeePassword.Equals(password))
                        {
                            Session["User"] = rowuser.EmployeeID;
                            Session["Name"] = rowuser.FirstName + " " + rowuser.LastName;
                            return RedirectToAction("Menu01", "Auth");
                        }
                        else
                        {
                            strerror = "Mật khẩu ko đúng " + password;
                        }
                    }
                    else
                    {
                        strerror = "Tên đăng nhập ko tồn tại";
                    }
                }
                else
                {
                    strerror = "CaptCha";
                }

            }
            ViewBag.Error = "<span class='text-danger'>" + strerror + "</span>";
            return View();
        }
      
        public ActionResult Logout()
        {
            Session["User"] = "";
            Session["Cus"] = "";
            Session["Name"] = "";
            Session["Ticket"] = null;
            return RedirectToAction("Login", "Auth");
        }
        public ActionResult Menu01()
        {
            var check = dbContext.Customers.Where(m => m.CustomerID == Session["User"]).FirstOrDefault();
            if (check != null)
            {
                return Content("<script language='javascript' type='text/javascript'>alert('Chưa có quyền!');window.location = '/Product/ListProducts'</script>");
            }
            return View();
        }
        public ActionResult TrangChu()
        {
            return View();
        }
        [AllowAnonymous]
        public bool ValidateCaptcha(string response)
        {
            string secret = ConfigurationManager.AppSettings["GoogleSecretkey"];
            var client = new WebClient();
            var reply = client.DownloadString(String.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));
            string url = "https://www.google.com/recaptcha/api/siteverify?secret=" + secret + "&response=" + response;
            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);
            return Convert.ToBoolean(captchaResponse.Success);
        }
    }
}
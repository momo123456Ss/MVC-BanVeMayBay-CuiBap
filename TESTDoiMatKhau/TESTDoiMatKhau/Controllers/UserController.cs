using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TESTDoiMatKhau.Models;

namespace TESTDoiMatKhau.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        ABCDataContext dbContext = new ABCDataContext();
        public ActionResult Index()
        {
            List<User> lstUser = dbContext.Users.Select(s => s).ToList();

            return View(lstUser);
        }
        public ActionResult ChangePass(string returnUrl)
        {
            ViewBag.Info = "";
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public ActionResult ChangePass(FormCollection field, string returnUrl)
        {
            string strerror = "";
            var rowuser = dbContext.Users.Where(m => m.username == field["username"]).FirstOrDefault();
            if (ModelState.IsValid)
            {
                if (rowuser == null)
                {
                    strerror = "User chưa có";
                }
                else
                {
                    try
                    {
                        rowuser.password = field["passwordchange"];
                        UpdateModel(rowuser);
                        dbContext.SubmitChanges();
                        strerror = "Kiểm tra đi";
                    }
                    catch
                    {

                    }
                }
            }
            ViewBag.Info = strerror;
            return View();
        }
        public ActionResult Create()
        {
            ViewBag.Create = "";
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            string strerror = "_";
            string usernane = collection["username"];
            string password = collection["password"];
            var rowuserID = dbContext.Users.Where(m => m.username == usernane).FirstOrDefault();
            if (rowuserID != null)
            {
                strerror = "Tồn tại ID dùng ID khác";
            }
            else
            {
                try
                {
                    User p = new User();
                    p.username = usernane;
                    p.password = password;
                    dbContext.Users.InsertOnSubmit(p);
                    dbContext.SubmitChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    strerror = "Lỗi";
                    return View() ;

                }
            }
            ViewBag.Create = "<span class='text-danger'>" + strerror + "</span>";
            return View();
        }
    }
}
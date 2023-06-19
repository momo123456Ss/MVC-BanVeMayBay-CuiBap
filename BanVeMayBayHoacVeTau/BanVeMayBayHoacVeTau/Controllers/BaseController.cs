using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanVeMayBayHoacVeTau.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        //public ActionResult Index()
        //{
        //    return View();
        //}
        public BaseController()
        {
            if (System.Web.HttpContext.Current.Session["User"].Equals(""))
            {
                System.Web.HttpContext.Current.Response.Redirect("../Auth/Login");
            }
            //else if (System.Web.HttpContext.Current.Session["Cus"].Equals(""))
            //{
            //    System.Web.HttpContext.Current.Response.Redirect("../Auth/Login");
            //}
        }
    }
}
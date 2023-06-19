using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
namespace BanVeMayBayHoacVeTau.Controllers
{
    public class CaptchaController : Controller
    {
        // GET: Captcha
        public ActionResult Index()
        {
            return View();
        }
        //need to write post here

        [HttpPost]
        public ActionResult FormSubmit()
        {
            //Validate Google recaptcha below

            var response = Request["g-recaptcha-response"];
            string secretKey = "6Let0N0kAAAAAFp-rLqZf7Sa1WcolgaN3jB7zOaE";
            var client = new WebClient();
            var result = client.DownloadString(String.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}",secretKey,response));
            ViewData["Message"] = "Google reCaptcha validation success";
            var obj = JObject.Parse(result);
            var status = (bool)obj.SelectToken("success");
            ViewBag.Message = status ? "Google reCaptcha success" : "Google reCaptcha failed";
            //Here I am returning to Index view:

            return View("Index");
        }
    }
}
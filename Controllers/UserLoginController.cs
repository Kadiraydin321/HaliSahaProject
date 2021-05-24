using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HaliSahaProject.Models;
using System.Data.Entity;

namespace HaliSahaProject.Controllers
{
    public class UserLoginController : Controller
    {
        HaliSahaDBEntities1 db = new HaliSahaDBEntities1();
        // GET: UserLogin
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index([Bind(Include = "Mail,Password")] UserLogin userLogin)
        {
            var _User = db.UserLogin.Where(x=>x.Mail.Equals(userLogin.Mail) && x.Password.Equals(userLogin.Password));
            if (_User.Count() > 0)
            {
                Session["UserID"] = _User.FirstOrDefault().User_ID;
                Session["UserMail"] = _User.FirstOrDefault().Mail;
                return RedirectToAction("Index", "Home");
            }
            ViewBag.GirisHatasi = "Mail veya şifreyi hatalı girdiniz.";
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session["UserID"] = null;
            Session["UserMail"] = null;
            return RedirectToAction("Index","Home");
        }
    }
}

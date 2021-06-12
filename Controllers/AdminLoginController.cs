using HaliSahaProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HaliSahaProject.Controllers
{
    public class AdminLoginController : Controller
    {
        HaliSahaDBEntities1 db = new HaliSahaDBEntities1();

        // GET: AdminLogin
        public ActionResult Index()
        {
            return View();
        }

        // POST: AdminLogin
        [HttpPost]
        public ActionResult Index(Admin admin)
        {
            var _admin = db.Admin.Where(x => x.UserName.Equals(admin.UserName) && x.Password.Equals(admin.Password));
            if (_admin.Count() > 0)
            {
                Session["Admin"] = _admin.FirstOrDefault().UserName;
                return RedirectToAction("Index", "Admins");
            }
            ViewBag.GirisHatasi = "Kullanıcı adı veya şifreyi hatalı girdiniz.";
            return View();
        }

        [HttpGet]
        public ActionResult AdminLogout()
        {
            Session["Admin"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}

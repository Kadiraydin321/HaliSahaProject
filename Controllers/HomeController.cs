using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HaliSahaProject.Models;

namespace HaliSahaProject.Controllers
{
    public class HomeController : Controller
    {
        private HaliSahaDBEntities1 db = new HaliSahaDBEntities1();

        public ActionResult Index(string search)
        {
            var astroturfs = db.Astroturfs.Include(a => a.Users).Include(a => a.Counties);
            astroturfs = astroturfs.Where(x => x.Name.Contains(search) || x.Adress.Contains(search)
                                            || x.Info.Contains(search) || x.Tel.Contains(search)
                                            || x.Counties.Name.Contains(search) || x.Users.Name.Contains(search)
                                            || search == null);
            return View(astroturfs);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}
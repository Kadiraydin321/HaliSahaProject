using HaliSahaProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace HaliSahaProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly HaliSahaDB db = new HaliSahaDB();

        [HttpGet]
        public ActionResult Index()
        {
            var tables = new CityViewModel
            {
                City = db.Cities.OrderBy(x => x.Name).ToList(),
                County= db.Counties.OrderBy(x => x.Name).ToList()
            };

            List<SelectListItem> iller = (from i in db.Cities.OrderBy(x => x.Name).ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.Name,
                                                 Value=i.ID.ToString()
                                             }
                                             ).ToList();
            ViewBag.il = iller;
            return View(tables);
        }
        [HttpPost]
        public ActionResult Index(CityViewModel City)
        {
            List<SelectListItem> ilceler = (from i in db.Counties.Where(x=>x.City_ID==).OrderBy(x => x.Name).ToList()
                                          select new SelectListItem
                                          {
                                              Text = i.Name,
                                              Value = i.ID.ToString()
                                          }
                                             ).ToList();
            ViewBag.ilce = ilceler;
            return View();
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
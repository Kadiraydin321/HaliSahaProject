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
    public class AstroturfsController : Controller
    {
        private HaliSahaDBEntities1 db = new HaliSahaDBEntities1();

        // GET: Astroturfs
        public ActionResult Index(string search)
        {
            var astroturfs = db.Astroturfs.Include(a => a.Users).Include(a => a.Counties);
            astroturfs = astroturfs.Where(x => x.Name.Contains(search) || x.Adress.Contains(search) 
                                            || x.Info.Contains(search) || x.Tel.Contains(search)
                                            || x.Counties.Name.Contains(search) || x.Users.Name.Contains(search)
                                            || search == null);
            return View(astroturfs);
        }

        // GET: Astroturfs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Astroturfs astroturfs = db.Astroturfs.Find(id);
            if (astroturfs == null)
            {
                return HttpNotFound();
            }
            return View(astroturfs);
        }

        // GET: Astroturfs/Create
        public ActionResult Create()
        {
            if (Session["admin"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var _users = db.Users.Where(x => x.Role_ID == 3);
            ViewBag.Manager_ID = new SelectList(_users, "ID", "Name");
            var _counties = from county in db.Counties join city in db.Cities on county.City_ID equals city.ID select new
                            {
                                ID = county.ID,
                                Name = city.Name + " - "+county.Name
                            };
            ViewBag.County_ID = new SelectList(_counties.OrderBy(x=>x.Name), "ID", "Name");
            return View();
        }

        // POST: Astroturfs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Adress,Tel,Info,County_ID,Manager_ID")] Astroturfs astroturfs)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Astroturfs.Add(astroturfs);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ViewBag.e = e.Message;
            }
            var _users = db.Users.Where(x => x.Role_ID == 3);
            ViewBag.Manager_ID = new SelectList(_users, "ID", "Name", astroturfs.Manager_ID);
            var _counties = from county in db.Counties
                            join city in db.Cities on county.City_ID equals city.ID
                            select new
                            {
                                ID = county.ID,
                                Name = city.Name + " - " + county.Name
                            };
            ViewBag.County_ID = new SelectList(_counties.OrderBy(x => x.Name), "ID", "Name", astroturfs.County_ID);
            return View(astroturfs);
        }

        // GET: Astroturfs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["admin"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Astroturfs astroturfs = db.Astroturfs.Find(id);
            if (astroturfs == null)
            {
                return HttpNotFound();
            }
            var _users = db.Users.Where(x => x.Role_ID == 3);
            ViewBag.Manager_ID = new SelectList(_users, "ID", "Name", astroturfs.Manager_ID);
            var _counties = from county in db.Counties
                            join city in db.Cities on county.City_ID equals city.ID
                            select new
                            {
                                ID = county.ID,
                                Name = city.Name + " - " + county.Name
                            };
            ViewBag.County_ID = new SelectList(_counties.OrderBy(x => x.Name), "ID", "Name", astroturfs.County_ID);
            return View(astroturfs);
        }

        // POST: Astroturfs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Adress,Tel,Info,County_ID,Manager_ID")] Astroturfs astroturfs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(astroturfs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var _users = db.Users.Where(x => x.Role_ID == 3);
            ViewBag.Manager_ID = new SelectList(_users, "ID", "Name", astroturfs.Manager_ID);
            var _counties = from county in db.Counties
                            join city in db.Cities on county.City_ID equals city.ID
                            select new
                            {
                                ID = county.ID,
                                Name = city.Name + " - " + county.Name
                            };
            ViewBag.County_ID = new SelectList(_counties.OrderBy(x => x.Name), "ID", "Name", astroturfs.County_ID);
            return View(astroturfs);
        }

        // GET: Astroturfs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["admin"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Astroturfs astroturfs = db.Astroturfs.Find(id);
            if (astroturfs == null)
            {
                return HttpNotFound();
            }
            return View(astroturfs);
        }

        // POST: Astroturfs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Astroturfs astroturfs = db.Astroturfs.Find(id);
            db.Astroturfs.Remove(astroturfs);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

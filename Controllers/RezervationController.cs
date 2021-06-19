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
    public class RezervationController : Controller
    {
        private HaliSahaDBEntities1 db = new HaliSahaDBEntities1();

        // GET: Rezervation
        public ActionResult Index()
        {
            var rezervations = db.Rezervations.Include(r => r.Astroturfs).Include(r => r.Users);
            if (Session["Admin"] == null)
            {
                if (Session["UserID"] == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                int usr_id = Convert.ToInt32(Session["UserID"].ToString());
                Users usrs = db.Users.Find(usr_id);
                if (usrs == null)
                {
                    return HttpNotFound();
                }
                else if (usrs.Role_ID == 3)
                {
                    rezervations = rezervations.Where(x=>x.Astroturfs.Manager_ID == usr_id).OrderBy(x=>x.State).ThenByDescending(x=>x.Date);
                    return View(rezervations);
                }
                else
                {
                    rezervations = rezervations.Where(x => x.User_ID == usr_id).OrderBy(x => x.State).ThenByDescending(x => x.Date);
                    return View(rezervations);
                }
            }
            return View(rezervations.OrderBy(x => x.State).ThenByDescending(x => x.Date));
        }

        // GET: Rezervation/Create
        public ActionResult Create(int? AstroID)
        {
            if (Session["UserID"] == null && Session["Admin"] == null)
            {
                return RedirectToAction("Index", "UserLogin");
            }
            if (AstroID == null)
            {
                ViewBag.Astroturf_ID = new SelectList(db.Astroturfs, "ID", "Name");
                return View();
            }
            ViewBag.Astroturf_ID = new SelectList(db.Astroturfs, "ID", "Name", AstroID);
            return View();
        }

        // POST: Rezervation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Date,Astroturf_ID,User_ID")] Rezervations rezervations)
        {
            if (ModelState.IsValid)
            {
                if (Session["Admin"]==null)
                {
                    db.Rezervations.Add(rezervations);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    rezervations.User_ID = db.Astroturfs.Where(x=>x.ID == rezervations.Astroturf_ID).FirstOrDefault().Manager_ID;
                    db.Rezervations.Add(rezervations);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Astroturf_ID = new SelectList(db.Astroturfs, "ID", "Name", rezervations.Astroturf_ID); 
            return View(rezervations);
        }
        // POST: Rezervation/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            Rezervations rezervations = db.Rezervations.Find(id);
            Users usr = db.Users.Find(rezervations.User_ID);
            usr.Point += 100;
            rezervations.State = true;
            db.Entry(rezervations).State = EntityState.Modified;
            db.Entry(usr).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Rezervation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rezervations rezervations = db.Rezervations.Find(id);
            if (rezervations == null)
            {
                return HttpNotFound();
            }
            return View(rezervations);
        }

        // POST: Rezervation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rezervations rezervations = db.Rezervations.Find(id);
            db.Rezervations.Remove(rezervations);
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

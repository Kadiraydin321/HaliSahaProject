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
                    ViewBag.user= "Manager";
                    rezervations = rezervations.Where(x=>x.User_ID == usr_id);
                    return View(rezervations);
                }
                else
                {
                    ViewBag.user = "User";
                    rezervations = rezervations.Where(x => x.User_ID == usr_id);
                    return View(rezervations);
                }
            }
            return View(rezervations);
        }

        // GET: Rezervation/Create
        public ActionResult Create(int? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "UserLogin");
            }
            if (id == null)
            {
                ViewBag.Astroturf_ID = new SelectList(db.Astroturfs, "ID", "Name");
                return View();
            }
            ViewBag.Astroturf_ID = new SelectList(db.Astroturfs, "ID", "Name", id);
            return View();
        }

        // POST: Rezervation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Date,Astroturf_ID")] Rezervations rezervations)
        {
            if (ModelState.IsValid)
            {
                if (Session["Admin"] == null)
                {
                    int userId = Convert.ToInt32(Session["UserID"].ToString());
                    rezervations.User_ID = userId;
                }
                rezervations.State = false;
                db.Rezervations.Add(rezervations);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Astroturf_ID = new SelectList(db.Astroturfs, "ID", "Name", rezervations.Astroturf_ID);
            return View(rezervations);
        }

        // GET: Rezervation/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.Astroturf_ID = new SelectList(db.Astroturfs, "ID", "Name", rezervations.Astroturf_ID);
            ViewBag.User_ID = new SelectList(db.Users, "ID", "Name", rezervations.User_ID);
            return View(rezervations);
        }

        // POST: Rezervation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Date,State,Astroturf_ID,User_ID")] Rezervations rezervations)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rezervations).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Astroturf_ID = new SelectList(db.Astroturfs, "ID", "Name", rezervations.Astroturf_ID);
            ViewBag.User_ID = new SelectList(db.Users, "ID", "Name", rezervations.User_ID);
            return View(rezervations);
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

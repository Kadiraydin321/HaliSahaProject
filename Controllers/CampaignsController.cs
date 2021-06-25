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
    public class CampaignsController : Controller
    {
        private HaliSahaDBEntities1 db = new HaliSahaDBEntities1();

        // GET: Campaigns
        public ActionResult Index()
        {
            return View(db.Campaign.ToList());
        }


        // GET: Campaigns/Create
        public ActionResult Create()
        {
            if (Session["Admin"] == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        // POST: Campaigns/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Info")] Campaign campaign)
        {
            if (ModelState.IsValid)
            {
                db.Campaign.Add(campaign);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(campaign);
        }

        // GET: Campaigns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["Admin"] == null)
            {
                return HttpNotFound();
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campaign campaign = db.Campaign.Find(id);
            if (campaign == null)
            {
                return HttpNotFound();
            }
            return View(campaign);
        }

        // POST: Campaigns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Campaign campaign = db.Campaign.Find(id);
            db.Campaign.Remove(campaign);
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

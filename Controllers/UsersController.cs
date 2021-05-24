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
    public class UsersController : Controller
    {
        private HaliSahaDBEntities1 db = new HaliSahaDBEntities1();

        // GET: Users
        public ActionResult Index()
        {
            if (Session["admin"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var users = db.Users.Include(u => u.Roles).Include(u => u.UserLogin);
            return View(users.ToList());
        }

        // GET: Users/Details
        public ActionResult Details()
        {
            if (Session["UserID"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(Session["UserID"]);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,LastName,Mail,Tel")] Users _users, [Bind(Include = "ID,Mail,Password")] UserLogin _UserLogin)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _users.RegisterDate = DateTime.Now;
                    _users.Point = 0;
                    _users.Role_ID = 1;

                    db.Users.Add(_users);
                    db.UserLogin.Add(_UserLogin);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.Role_ID = new SelectList(db.Roles, "ID", "Name", _users.Role_ID);
                ViewBag.ID = new SelectList(db.UserLogin, "User_ID", "Mail", _users.ID);
                ViewBag.Basarili = "Başarılı bir şekilde kayıt oldunuz.";
                return View(_users);
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                ViewBag.KayitHatasi="Bu mail ile önceden kayıt oluşturulmuş.";
                return View();
            }
        }

        // GET: Users/Edit
        public ActionResult Edit()
        {
            if (Session["UserID"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(Session["UserID"]);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Name,LastName,Mail,Tel")] Users _user, String Mail)
        {
            UserLogin userLogin = db.UserLogin.Find(Session["UserID"]);
            Users user = db.Users.Find(Session["UserID"]);
            if (ModelState.IsValid)
            {
                user.Name = _user.Name;
                user.LastName = _user.LastName;
                user.Mail = _user.Mail;
                user.Tel = _user.Tel;

                userLogin.Mail = Mail;

                db.Entry(user).State = EntityState.Modified;
                db.Entry(userLogin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details");
            }
            return View(_user);
        }

        // GET: Users/PasswordEdit
        public ActionResult PasswordEdit()
        {
            if (Session["UserID"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserLogin userLogin = db.UserLogin.Find(Session["UserID"]);
            if (userLogin == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        // POST: Users/PasswordEdit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PasswordEdit(String NewPass, String OldPass)
        {
            UserLogin userLogin = db.UserLogin.Find(Session["UserID"]);
            if (ModelState.IsValid)
            {
                if (userLogin.Password != OldPass)
                {
                    ViewBag.GSifreHata = "Eski şifrenizi yanlış girdiniz.";
                    return View();
                }
                else
                {
                    userLogin.Password = NewPass;
                    db.Entry(userLogin).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Edit","Users",new { id=userLogin.User_ID });
                }
            }
            return RedirectToAction("Edit", "Users", userLogin.User_ID);
        }

        // GET: Users/RoleEdit
        public ActionResult RoleEdit(int? id)
        {
            if (Session["admin"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users user= db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            List<SelectListItem> roller = new List<SelectListItem>();
            foreach (var item in db.Roles.ToList())
            {
                if (item.ID == user.Role_ID)
                {
                    roller.Add(new SelectListItem { Text = item.Name, Value = item.ID.ToString(),Selected=true });
                }
                else
                {
                    roller.Add(new SelectListItem { Text = item.Name, Value = item.ID.ToString()});
                }
            }
            ViewBag.Roller = roller;
            return View(user);
        }

        // POST: Users/RoleEdit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleEdit(int Role_ID, int id)
        {
            
            if (ModelState.IsValid)
            {
                Users user = db.Users.Find(id);
                user.Role_ID = Role_ID;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Hata = "Rol Düzenleme Yapılamadı.";
            return View();
        }

        // GET: Users/Delete/5
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
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users users = db.Users.Find(id);
            UserLogin userLogin = db.UserLogin.Find(id);
            db.Users.Remove(users);
            db.UserLogin.Remove(userLogin);
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

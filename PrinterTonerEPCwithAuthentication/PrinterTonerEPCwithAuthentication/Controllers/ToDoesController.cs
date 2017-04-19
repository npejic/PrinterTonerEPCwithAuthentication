using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PrinterTonerEPCwithAuthentication.Models;

namespace PrinterTonerEPCwithAuthentication.Controllers

{
    public class ToDoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        public ActionResult Index()
        {
            var toDoes = db.ToDoes.Include(t => t.ApplicationUser).OrderBy(c => c.Closed != null).ThenByDescending(c => c.Closed).ThenBy(c => c.Created);
            return View(toDoes.ToList());
        }

        public ActionResult Create()
        {

            var sortedUsers = db.Users.OrderBy(c => c.Nick);
            ViewBag.ApplicationUserID = new SelectList(sortedUsers, "Id", "Nick");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ToDoID,Description,Closed,ApplicationUserID,IsReady")] ToDo toDo)
        {
            if (ModelState.IsValid)
            {
                db.ToDoes.Add(toDo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Users, "Id", "Nick", toDo.ApplicationUser);
            return View(toDo);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDo toDo = db.ToDoes.Find(id);
            if (toDo == null)
            {
                return HttpNotFound();
            }

            var sortedUsers = db.Users.OrderBy(c => c.Nick);
            ViewBag.ApplicationUserID = new SelectList(sortedUsers, "Id", "Nick");
            //ViewBag.UserID = new SelectList(sortedUsers, "UserID", "Nick", toDo.ApplicationUser.Id);
            return View(toDo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ToDoID,Description,Closed,ApplicationUserID,IsReady")] ToDo toDo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(toDo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Nick", toDo.ApplicationUser.Id);
            return View(toDo);
        }

        public ActionResult EditFromHomeView(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDo toDo = db.ToDoes.Find(id);
            if (toDo == null)
            {
                return HttpNotFound();
            }

            var sortedUsers = db.Users.OrderBy(c => c.Nick);
            ViewBag.UserID = new SelectList(sortedUsers, "UserID", "Nick", toDo.ApplicationUser.Id);
            return View(toDo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditFromHomeView([Bind(Include = "ToDoID,Description,Closed,UserID,IsReady")] ToDo toDo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(toDo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Nick", toDo.ApplicationUser.Id);
            return View(toDo);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDo toDo = db.ToDoes.Find(id);
            if (toDo == null)
            {
                return HttpNotFound();
            }
            return View(toDo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ToDo toDo = db.ToDoes.Find(id);
            db.ToDoes.Remove(toDo);
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

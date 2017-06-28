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
    public class MakeTonersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MakeToners
        public ActionResult Index()
        {
            var makeToners = db.MakeToners.Include(m => m.Owner).Include(m => m.Toner);
            return View(makeToners.ToList());
        }

        // GET: MakeToners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MakeToner makeToner = db.MakeToners.Find(id);
            if (makeToner == null)
            {
                return HttpNotFound();
            }
            return View(makeToner);
        }

        // GET: MakeToners/Create
        public ActionResult Create()
        {
            var orderedOwners = db.Owners.OrderBy(c => c.OwnerName);
            ViewBag.OwnerID = new SelectList(orderedOwners, "OwnerID", "OwnerName");

            var orderedToners = db.Toners.OrderBy(c => c.TonerModel);
            ViewBag.TonerID = new SelectList(orderedToners, "TonerID", "TonerModel");
            return View();
        }

        // POST: MakeToners/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MakeTonerID,MakeTonerDate,MakeTonerQuantity,MakeTonerPrice,Remark,TonerID,OwnerID")] MakeToner makeToner)
        {
            if (ModelState.IsValid)
            {
                db.MakeToners.Add(makeToner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OwnerID = new SelectList(db.Owners, "OwnerID", "OwnerName", makeToner.OwnerID);
            ViewBag.TonerID = new SelectList(db.Toners, "TonerID", "TonerModel", makeToner.TonerID);
            return View(makeToner);
        }

        // GET: MakeToners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MakeToner makeToner = db.MakeToners.Find(id);
            if (makeToner == null)
            {
                return HttpNotFound();
            }
            ViewBag.OwnerID = new SelectList(db.Owners, "OwnerID", "OwnerName", makeToner.OwnerID);
            ViewBag.TonerID = new SelectList(db.Toners, "TonerID", "TonerModel", makeToner.TonerID);
            return View(makeToner);
        }

        // POST: MakeToners/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MakeTonerID,MakeTonerDate,MakeTonerQuantity,MakeTonerPrice,Remark,TonerID,OwnerID")] MakeToner makeToner)
        {
            if (ModelState.IsValid)
            {
                db.Entry(makeToner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerID = new SelectList(db.Owners, "OwnerID", "OwnerName", makeToner.OwnerID);
            ViewBag.TonerID = new SelectList(db.Toners, "TonerID", "TonerModel", makeToner.TonerID);
            return View(makeToner);
        }

        // GET: MakeToners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MakeToner makeToner = db.MakeToners.Find(id);
            if (makeToner == null)
            {
                return HttpNotFound();
            }
            return View(makeToner);
        }

        // POST: MakeToners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MakeToner makeToner = db.MakeToners.Find(id);
            db.MakeToners.Remove(makeToner);
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

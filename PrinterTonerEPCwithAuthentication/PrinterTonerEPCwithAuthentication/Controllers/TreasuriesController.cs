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
    public class TreasuriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Treasuries
        public ActionResult Index()
        {
            //TempData["treasurySumRSD"] = db.Treasuries.Sum(c => c.AmountRSD);
            //TempData["treasurySumEUR"] = db.Treasuries.Sum(c => c.AmountEUR);
            
            var treasuries = db.Treasuries;//.Include(t => t.ApplicationUser);
            return View(treasuries.ToList());
        }

        // GET: Treasuries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treasury treasury = db.Treasuries.Find(id);
            if (treasury == null)
            {
                return HttpNotFound();
            }
            return View(treasury);
        }

        // GET: Treasuries/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "FullName");
            return View();
        }

        // POST: Treasuries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TreasuryID,ApplicationUserID,AmountRSD,AmountEUR,Expence,Remark,Created")] Treasury treasury)
        {
            if (ModelState.IsValid)
            {
                db.Treasuries.Add(treasury);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "FullName", treasury.ApplicationUserID);
            return View(treasury);
        }

        // GET: Treasuries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treasury treasury = db.Treasuries.Find(id);
            if (treasury == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "FullName", treasury.ApplicationUserID);
            return View(treasury);
        }

        // POST: Treasuries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TreasuryID,ApplicationUserID,AmountRSD,AmountEUR,Expence,Remark,Created")] Treasury treasury)
        {
            if (ModelState.IsValid)
            {
                db.Entry(treasury).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "FullName", treasury.ApplicationUserID);
            return View(treasury);
        }

        // GET: Treasuries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treasury treasury = db.Treasuries.Find(id);
            if (treasury == null)
            {
                return HttpNotFound();
            }
            return View(treasury);
        }

        // POST: Treasuries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Treasury treasury = db.Treasuries.Find(id);
            db.Treasuries.Remove(treasury);
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

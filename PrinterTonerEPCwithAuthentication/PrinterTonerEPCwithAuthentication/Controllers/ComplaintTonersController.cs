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
    public class ComplaintTonersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ComplaintToners
        public ActionResult Index()
        {
            var complaintToners = db.ComplaintToners.Include(c => c.Toner);
            return View(complaintToners.ToList());
        }

        // GET: ComplaintToners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComplaintToner complaintToner = db.ComplaintToners.Find(id);
            if (complaintToner == null)
            {
                return HttpNotFound();
            }
            return View(complaintToner);
        }

        // GET: ComplaintToners/Create
        public ActionResult Create()
        {
            ViewBag.TonerID = new SelectList(db.Toners, "TonerID", "TonerModel");
            return View();
        }

        // POST: ComplaintToners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ComplaintTonerID,ComplaintTonerDate,TonerID,Remark,Created")] ComplaintToner complaintToner)
        {
            if (ModelState.IsValid)
            {
                db.ComplaintToners.Add(complaintToner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TonerID = new SelectList(db.Toners, "TonerID", "TonerModel", complaintToner.TonerID);
            return View(complaintToner);
        }

        // GET: ComplaintToners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComplaintToner complaintToner = db.ComplaintToners.Find(id);
            if (complaintToner == null)
            {
                return HttpNotFound();
            }
            ViewBag.TonerID = new SelectList(db.Toners, "TonerID", "TonerModel", complaintToner.TonerID);
            return View(complaintToner);
        }

        // POST: ComplaintToners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ComplaintTonerID,ComplaintTonerDate,TonerID,Remark,Created")] ComplaintToner complaintToner)
        {
            if (ModelState.IsValid)
            {
                db.Entry(complaintToner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TonerID = new SelectList(db.Toners, "TonerID", "TonerModel", complaintToner.TonerID);
            return View(complaintToner);
        }

        // GET: ComplaintToners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComplaintToner complaintToner = db.ComplaintToners.Find(id);
            if (complaintToner == null)
            {
                return HttpNotFound();
            }
            return View(complaintToner);
        }

        // POST: ComplaintToners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ComplaintToner complaintToner = db.ComplaintToners.Find(id);
            db.ComplaintToners.Remove(complaintToner);
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

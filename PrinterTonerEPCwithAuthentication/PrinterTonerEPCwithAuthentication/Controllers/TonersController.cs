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
    public class TonersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var toners = db.Toners.OrderBy(t => t.TonerModel).ThenBy(t=>t.TonerYield).ThenBy(t=>t.TonerGram);

            return View(toners.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Toner toner = db.Toners.Find(id);
            if (toner == null)
            {
                return HttpNotFound();
            }
            return View(toner);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TonerID,TonerModel,TonerIsOriginal,TonerYield,TonerProductNo,TonerGram,TonerMinQuantity,Created")] Toner toner)
        {
            if (ModelState.IsValid)
            {
                
                db.Toners.Add(toner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(toner);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Toner toner = db.Toners.Find(id);
            if (toner == null)
            {
                return HttpNotFound();
            }
            return View(toner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TonerID,TonerModel,TonerIsOriginal,TonerYield,TonerGram,TonerProductNo,TonerMinQuantity,Created")] Toner toner)
        {
            if (ModelState.IsValid)
            {
                db.Entry(toner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(toner);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Toner toner = db.Toners.Find(id);
            if (toner == null)
            {
                return HttpNotFound();
            }
            return View(toner);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Toner toner = db.Toners.Find(id);
            db.Toners.Remove(toner);
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

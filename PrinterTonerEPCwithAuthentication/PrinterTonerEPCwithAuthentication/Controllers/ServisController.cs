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
    [Authorize(Roles = "user")]
    public class ServisController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        int selectedOwnerForServis;

        public ActionResult Index(string searchBySerialNo, string searchByOwner)
        {
            var servis = db.Servis.Include(s => s.Printer).OrderByDescending(o => o.ServisDate);//.OrderBy(s => s.Printer.Owner.OwnerName).ThenBy(o => o.ServisDate);

            if (!String.IsNullOrEmpty(searchBySerialNo))
            {
                servis = servis.Where(o => o.Printer.PrinterSerialNo.Contains(searchBySerialNo)).OrderByDescending(o => o.ServisDate);//.OrderBy(s => s.Printer.Owner.OwnerName).ThenBy(o => o.ServisDate);
            }

            if (!String.IsNullOrEmpty(searchByOwner))
            {
                servis = servis.Where(o => o.Printer.Owner.OwnerName.Contains(searchByOwner)).OrderByDescending(o => o.ServisDate);//.OrderBy(s => s.Printer.Owner.OwnerName).ThenBy(o => o.ServisDate);
            }

            return View(servis.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servis servis = db.Servis.Find(id);
            if (servis == null)
            {
                return HttpNotFound();
            }
            return View(servis);
        }

        public ActionResult Create()
        {
            ViewBag.OwnerID = new SelectList(db.Owners, "OwnerID", "OwnerName"); //TODO: ovo ne treba
            
            var chosenOwner = (int)TempData["OwnerID"];
            var printersBySelectedOwner = db.Printers.Where(p => p.OwnerID == chosenOwner);
            
            ViewBag.PrinterID = new SelectList(printersBySelectedOwner, "PrinterID", "PrinterSerialNo");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ServisID,ServisDate,ServisPrice,PrinterID,Remark,Created")] Servis servis)
        {
            if (ModelState.IsValid)
            {
                db.Servis.Add(servis);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PrinterID = new SelectList(db.Printers, "PrinterID", "PrinterSerialNo", servis.PrinterID);
            return View(servis);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servis servis = db.Servis.Find(id);
            if (servis == null)
            {
                return HttpNotFound();
            }
            ViewBag.PrinterID = new SelectList(db.Printers, "PrinterID", "PrinterSerialNo", servis.PrinterID);
            return View(servis);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ServisID,ServisDate,ServisPrice,PrinterID,Remark,Created")] Servis servis)
        {
            if (ModelState.IsValid)
            {
                db.Entry(servis).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PrinterID = new SelectList(db.Printers, "PrinterID", "PrinterSerialNo", servis.PrinterID);
            return View(servis);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servis servis = db.Servis.Find(id);
            if (servis == null)
            {
                return HttpNotFound();
            }
            return View(servis);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Servis servis = db.Servis.Find(id);
            db.Servis.Remove(servis);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SelectOwner()
        {
            var orderedOwners = db.Owners.OrderBy(c => c.OwnerName);
            ViewBag.OwnerID = new SelectList(orderedOwners, "OwnerID", "OwnerName");
            
            return View();
        }

        [HttpPost]
        public ActionResult SelectOwner(Owner model)
        {
            selectedOwnerForServis = model.OwnerID;
            TempData["OwnerID"] = model.OwnerID;
            return RedirectToAction("Create");
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

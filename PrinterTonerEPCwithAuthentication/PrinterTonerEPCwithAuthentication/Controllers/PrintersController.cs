using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PrinterTonerEPCwithAuthentication.Models;
using System.Data.Entity.Infrastructure;

namespace PrinterTonerEPCwithAuthentication.Controllers
{
    [Authorize(Roles = "user")]
    public class PrintersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var sortedPrinters = db.Printers.OrderBy(p => p.PrinterManufacturer).ThenBy(p => p.PrinterModel).ThenBy(p => p.PrinterSerialNo);
            return View(sortedPrinters.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Printer printer = db.Printers.Find(id);
            if (printer == null)
            {
                return HttpNotFound();
            }
            return View(printer);
        }

        public ActionResult Create()
        {
            var orderedOwners = db.Owners.OrderBy(n => n.OwnerName);
            ViewBag.OwnerID = new SelectList(orderedOwners, "OwnerID", "OwnerName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PrinterID,OwnerID,PrinterInternalNo,PrinterManufacturer,PrinterModel,PrinterSerialNo,PrinterHardwareNo,PrinterDecommissioned,IsEPCprinter,Created")] Printer printer)
        {
            if (ModelState.IsValid)
            {
                //TODO:
                try
                {
                    db.Printers.Add(printer);
                    db.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "Printers", "Create"));
                    //return HttpNotFound("ooops, there is no page like this :/");
                    //ViewBag.printerError = "Štampač već postoji u bazi!";
                    //Response.Redirect("/Shared/Error.cshtml");
                }
                catch (Exception e)
                {
                    return View("Error", new HandleErrorInfo(e, "Printers", "Create"));
                }
                ///////////////////////

                return RedirectToAction("Index");
            }

            return View(printer);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Printer printer = db.Printers.Find(id);
            if (printer == null)
            {
                return HttpNotFound();
            }
            var orderedOwners = db.Owners.OrderBy(n => n.OwnerName);
            ViewBag.OwnerID = new SelectList(orderedOwners, "OwnerID", "OwnerName", printer.OwnerID);
            return View(printer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PrinterID,OwnerID,PrinterInternalNo,PrinterManufacturer,PrinterModel,PrinterSerialNo,PrinterHardwareNo,PrinterDecommissioned,IsEPCprinter,Created")] Printer printer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(printer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerID = new SelectList(db.Owners, "OwnerID", "OwnerName", printer.OwnerID);
            return View(printer);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Printer printer = db.Printers.Find(id);
            if (printer == null)
            {
                return HttpNotFound();
            }
            return View(printer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Printer printer = db.Printers.Find(id);
            db.Printers.Remove(printer);
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

 
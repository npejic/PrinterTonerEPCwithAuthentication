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
    public class SaleTonersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            //var saleToners = db.SaleToners.Include(s => s.Owner).Include(s => s.Toner)
            //                    .OrderBy(s => s.Owner.OwnerName).ThenBy(s => s.Toner.TonerModel).ThenBy(s => s.SaleTonerDate);

            var saleToners = db.SaleToners.Include(s => s.Owner).Include(s => s.Toner).OrderByDescending(s => s.SaleTonerDate).ThenBy(c=>c.Owner.OwnerName);

            return View(saleToners.ToList());
        }

        //Returns list of owners (companies) that didn't order toners in last X (periodInMonths) months
        //Report No.5
        public ActionResult TonerAlarm(string periodInMonths)
        {
            var ownersWithNoAlarmOrder = db.SaleToners
                  .Where(c => c.Owner.OwnerIsActive == true)
                  .GroupBy(c => c.OwnerID)
                  .Select(s => s.OrderByDescending(x => x.SaleTonerDate).FirstOrDefault()).OrderBy(c => c.SaleTonerDate);


            if (!String.IsNullOrEmpty(periodInMonths))
            {
                int period = Int16.Parse(periodInMonths);
                var LimitDate = DateTime.Now.Date;
                LimitDate = LimitDate.AddMonths(-period);
                ownersWithNoAlarmOrder = ownersWithNoAlarmOrder.Where(o => o.SaleTonerDate < LimitDate).OrderBy(s => s.SaleTonerDate);
            }
            return View(ownersWithNoAlarmOrder.ToList());
        }

        //Report No.6
        public ActionResult TotalTonerSale(string dateFromString, string dateToString)
        {
            var soldToners = db.SaleToners.GroupBy(r => r.Toner.TonerModel).Select(r => new TonerTotal()
            {
                TotalTonerModel = r.Key,
                TonerTotalCount = r.Sum(c=> c.TonerQuantity),
            }).OrderByDescending(c => c.TonerTotalCount).ToList();
            
            //TODO: missing part of the code which will do TryParse DateTime input
            if (!String.IsNullOrEmpty(dateFromString) || !String.IsNullOrEmpty(dateToString))
            {
                DateTime dateFrom = Convert.ToDateTime(dateFromString);
                //if (DateTime.TryParse(dateFromString, out dateFrom))
                //{

                //}
                DateTime dateTo = Convert.ToDateTime(dateToString);
                var soldTonersInPeriod = db.SaleToners.Where(c => c.SaleTonerDate >= dateFrom && c.SaleTonerDate <= dateTo).GroupBy(r => r.Toner.TonerModel).Select(r => new TonerTotal()
                {
                    TotalTonerModel = r.Key,
                    TonerTotalCount = r.Sum(c => c.TonerQuantity),
                }).OrderByDescending(c => c.TonerTotalCount).ToList();

                soldToners = soldTonersInPeriod;
            }
            
            //Izračunava ukupan broj EPC štampača na iznajmljivanju
            var CountSoldToners = soldToners.Sum(c=>c.TonerTotalCount);
            ViewData["CountSoldToners"] = CountSoldToners;

            return View(soldToners.ToList());
        }

        //Report No.4
        public ActionResult LastTonerSale(string searchByOwner, string searchByToner)
        {
            var lastTonerSale = db.SaleToners.Where(c => c.Owner.OwnerIsActive == true).GroupBy(g => new { g.Owner.OwnerName, g.TonerID }).Select(s => s.OrderByDescending(x => x.SaleTonerDate).FirstOrDefault()).OrderBy(s => s.Owner.OwnerName).ThenBy(s => s.Toner.TonerModel);

            if (!String.IsNullOrEmpty(searchByOwner))
            {
                lastTonerSale = lastTonerSale.Where(o => o.Owner.OwnerName.Contains(searchByOwner)).OrderBy(s => s.Owner.OwnerName).ThenBy(s => s.Toner.TonerModel).ThenBy(s => s.SaleTonerDate);
            }

            if (!String.IsNullOrEmpty(searchByToner))
            {
                lastTonerSale = lastTonerSale.Where(o => o.Toner.TonerModel.Contains(searchByToner)).OrderBy(s => s.Owner.OwnerName).ThenBy(s => s.Toner.TonerModel).ThenBy(s => s.SaleTonerDate);
            }

            return View(lastTonerSale.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleToner saleToner = db.SaleToners.Find(id);
            if (saleToner == null)
            {
                return HttpNotFound();
            }
            return View(saleToner);
        }

        public ActionResult Create()
        {
            var orderedOwners = db.Owners.OrderBy(c => c.OwnerName);
            ViewBag.OwnerID = new SelectList(orderedOwners, "OwnerID", "OwnerName");

            var orderedToners = db.Toners.OrderBy(c => c.TonerModel);
            ViewBag.TonerID = new SelectList(orderedToners, "TonerID", "TonerModel");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SaleTonerID,SaleTonerDate,TonerPrice,OwnerID,TonerID,TonerQuantity,InvoiceNo")] SaleToner saleToner)
        {
            if (ModelState.IsValid)
            {
                db.SaleToners.Add(saleToner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OwnerID = new SelectList(db.Owners, "OwnerID", "OwnerName", saleToner.OwnerID);
            ViewBag.TonerID = new SelectList(db.Toners, "TonerID", "TonerModel", saleToner.TonerID);
            return View(saleToner);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleToner saleToner = db.SaleToners.Find(id);
            if (saleToner == null)
            {
                return HttpNotFound();
            }
            ViewBag.OwnerID = new SelectList(db.Owners, "OwnerID", "OwnerName", saleToner.OwnerID);
            ViewBag.TonerID = new SelectList(db.Toners, "TonerID", "TonerModel", saleToner.TonerID);
            return View(saleToner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SaleTonerID,SaleTonerDate,TonerPrice,OwnerID,TonerID,TonerQuantity,InvoiceNo")] SaleToner saleToner)
        {
            if (ModelState.IsValid)
            {
                db.Entry(saleToner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerID = new SelectList(db.Owners, "OwnerID", "OwnerName", saleToner.OwnerID);
            ViewBag.TonerID = new SelectList(db.Toners, "TonerID", "TonerModel", saleToner.TonerID);
            return View(saleToner);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleToner saleToner = db.SaleToners.Find(id);
            if (saleToner == null)
            {
                return HttpNotFound();
            }
            return View(saleToner);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SaleToner saleToner = db.SaleToners.Find(id);
            db.SaleToners.Remove(saleToner);
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

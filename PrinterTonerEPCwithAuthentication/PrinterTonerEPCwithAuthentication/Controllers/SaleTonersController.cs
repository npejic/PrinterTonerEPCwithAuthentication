using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PrinterTonerEPCwithAuthentication.Models;
using PrinterTonerEPCwithAuthentication.Common;

namespace PrinterTonerEPCwithAuthentication.Controllers
{
    public class SaleTonersController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            //returns ordered list of sold toners first by date and then by owner
            var saleToners = ControllerMethods.OrderedListOfSoldTonersFirstByDateAndThenByOwner();

            return View(saleToners.ToList());
        }

        //Report No.8 - number of all toner models in warehouse 
        public ActionResult WarehouseToner()
        {
            var differences = ControllerMethods.DifferencesBetweenSoldAndMadeToners();
            return View(differences);

        }
        
        //Report No.7 - daily directive for making toners which quantity is bellow the minimal requirement
        public ActionResult DailyDirective()
        {
           
            var differences = ControllerMethods.DifferencesBetweenSoldAndMadeToners().Where(c => c.TonerTotalCount <= c.TonerTotalMin && c.TonerTotalCount != c.TonerTotalMin).OrderByDescending(c => c.TonerTotalMin - c.TonerTotalCount).ThenBy(c => c.TotalTonerModel);
            return View(differences);
        }

        //Report No.5 - Returns list of owners (companies) that didn't order toners in last X (periodInMonths) months
        public ActionResult TonerAlarm(string periodInMonths)
        {
            #region za brisati
            //var ownersWithNoAlarmOrder = db.SaleToners
            //      .Where(c => c.Owner.OwnerIsActive == true)
            //      .GroupBy(c => c.OwnerID)
            //      .Select(s => s.OrderByDescending(x => x.SaleTonerDate).FirstOrDefault()).OrderBy(c => c.SaleTonerDate);


            //if (!String.IsNullOrEmpty(periodInMonths))
            //{
            //    int period = Int16.Parse(periodInMonths);
            //    var LimitDate = DateTime.Now.Date;
            //    LimitDate = LimitDate.AddMonths(-period);
            //    ownersWithNoAlarmOrder = ownersWithNoAlarmOrder.Where(o => o.SaleTonerDate < LimitDate).OrderBy(s => s.SaleTonerDate);
            //}
            #endregion
            var ownersWithNoAlarmOrder = ControllerMethods.OwnersWithNoTonerOrderInSomePeriod(periodInMonths);
            return View(ownersWithNoAlarmOrder.ToList());
        }

        //Report No.5a - list of companies that didn't order any toner
        public ActionResult TonerAlarm2()
        {
            var ownersListWithNoOrder = ControllerMethods.OwnersWithNoTonerOrder();
            return View(ownersListWithNoOrder.ToList());
        }


        //Report No.6 - total sum of sold toners by sorted by model
        public ActionResult TotalTonerSale(string dateFromString, string dateToString)
        {
            #region za brisati
            //var soldToners = db.SaleToners.GroupBy(r => r.Toner.TonerModel).Select(r => new TonerTotal()
            //{
            //    TotalTonerModel = r.Key,
            //    TonerTotalCount = r.Sum(c => c.TonerQuantity),
            //}).OrderByDescending(c => c.TonerTotalCount).ToList();

            ////TODO: missing part of the code which will do TryParse DateTime input
            //if (!String.IsNullOrEmpty(dateFromString) || !String.IsNullOrEmpty(dateToString))
            //{
            //    DateTime dateFrom = Convert.ToDateTime(dateFromString);
            //    //if (DateTime.TryParse(dateFromString, out dateFrom))
            //    //{

            //    //}
            //    DateTime dateTo = Convert.ToDateTime(dateToString);
            //    var soldTonersInPeriod = db.SaleToners.Where(c => c.SaleTonerDate >= dateFrom && c.SaleTonerDate <= dateTo).GroupBy(r => r.Toner.TonerModel).Select(r => new TonerTotal()
            //    {
            //        TotalTonerModel = r.Key,
            //        TonerTotalCount = r.Sum(c => c.TonerQuantity),
            //    }).OrderByDescending(c => c.TonerTotalCount).ToList();

            //    soldToners = soldTonersInPeriod;
            //}
            #endregion
            var soldToners = ControllerMethods.ListOfAllSoldTonersInPeriod(dateFromString, dateToString);

            //Puts total sum of sold toners in period to ViewData
            ViewData["CountSoldToners"] = ControllerMethods.SumOfAllSoldTonersInPeriod(soldToners);

            return View(soldToners.ToList());
        }

        //Report No.4
        public ActionResult LastTonerSale(string searchByOwner, string searchByToner)
        {
            #region za brisati
            //var lastTonerSale = db.SaleToners.Where(c => c.Owner.OwnerIsActive == true).GroupBy(g => new { g.Owner.OwnerName, g.TonerID })
            //    .Select(s => s.OrderByDescending(x => x.SaleTonerDate).FirstOrDefault()).OrderBy(s => s.Owner.OwnerName).ThenBy(s => s.Toner.TonerModel).ToList();

            //if (!String.IsNullOrEmpty(searchByOwner))
            //{
            //    lastTonerSale = lastTonerSale.Where(o => o.Owner.OwnerName.Contains(searchByOwner)).OrderBy(s => s.Owner.OwnerName).ThenBy(s => s.Toner.TonerModel).ThenBy(s => s.SaleTonerDate);
            //}

            //if (!String.IsNullOrEmpty(searchByToner))
            //{
            //    lastTonerSale = lastTonerSale.Where(o => o.Toner.TonerModel.Contains(searchByToner)).OrderBy(s => s.Owner.OwnerName).ThenBy(s => s.Toner.TonerModel).ThenBy(s => s.SaleTonerDate);
            //}
            #endregion
            var lastTonerSale = ControllerMethods.LastTonerSoldByOwnerOrTonerModel(searchByOwner, searchByToner);
            return View(lastTonerSale.ToList());
        }

        public ActionResult Create()
        {
            ApplicationDbContext db = new ApplicationDbContext();

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
            ApplicationDbContext db = new ApplicationDbContext();

            if (ModelState.IsValid)
            {
                db.SaleToners.Add(saleToner);
                db.SaveChanges();
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                LogJobs.LogSuccess(saleToner.SaleTonerID.ToString(), controllerName, actionName);
                return RedirectToAction("Index");
            }

            ViewBag.OwnerID = new SelectList(db.Owners, "OwnerID", "OwnerName", saleToner.OwnerID);
            ViewBag.TonerID = new SelectList(db.Toners, "TonerID", "TonerModel", saleToner.TonerID);
            return View(saleToner);
        }

        public ActionResult Edit(int? id)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleToner saleToner = db.SaleToners.Find(id);
            if (saleToner == null)
            {
                return HttpNotFound();
            }
            //TODO:promenjeno
            var orderedOwners = db.Owners.OrderBy(c => c.OwnerName);
            ViewBag.OwnerID = new SelectList(orderedOwners, "OwnerID", "OwnerName");

            var orderedToners = db.Toners.OrderBy(c => c.TonerModel);
            ViewBag.TonerID = new SelectList(orderedToners, "TonerID", "TonerModel");
            
            //ViewBag.OwnerID = new SelectList(db.Owners, "OwnerID", "OwnerName", saleToner.OwnerID);
            //ViewBag.TonerID = new SelectList(db.Toners, "TonerID", "TonerModel", saleToner.TonerID);
            return View(saleToner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SaleTonerID,SaleTonerDate,TonerPrice,OwnerID,TonerID,TonerQuantity,InvoiceNo")] SaleToner saleToner)
        {
            ApplicationDbContext db = new ApplicationDbContext();

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
            ApplicationDbContext db = new ApplicationDbContext();

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
            ApplicationDbContext db = new ApplicationDbContext();

            SaleToner saleToner = db.SaleToners.Find(id);
            db.SaleToners.Remove(saleToner);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

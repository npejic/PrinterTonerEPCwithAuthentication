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
    public class ContractsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// All contracts
        /// </summary>
        /// <returns>list of contracts ordered by Owner name and Date</returns>
        public ActionResult Index()
        {
            var contracts = db.Contracts.Include(c => c.Owner).OrderBy(c=>c.Owner.OwnerName).ThenBy(c=>c.ContractDate);
            return View(contracts.ToList());
        }

        /// <summary>
        /// Report No.2 for contracts that are no longer active
        /// </summary>
        /// <returns>sends inactiveOwners list to View</returns>
        public ActionResult InactiveOwners()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var inactiveOwners = from i in db.Contracts select i;
            var Today= DateTime.Now.Date; //.Date is not suported by LINQ
            
            inactiveOwners = inactiveOwners.Where(a=>DbFunctions.AddMonths(a.ContractDate,a.ContactDuration) < Today );

            return View(inactiveOwners.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            return View(contract);
        }

        public ActionResult Create()
        {
            ViewBag.OwnerID = new SelectList(db.Owners, "OwnerID", "OwnerName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContractID,ContractName,OwnerID,ContractIs,ContactDuration,ContractComplete,ContractDate,ContractActive,Created")] Contract contract)
        {
            if (ModelState.IsValid)
            {
                db.Contracts.Add(contract);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.OwnerID = new SelectList(db.Owners, "OwnerID", "OwnerName", contract.OwnerID);
            return View(contract);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            ViewBag.OwnerID = new SelectList(db.Owners, "OwnerID", "OwnerName", contract.OwnerID);
            return View(contract);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContractID,ContractName,OwnerID,ContractIs,ContactDuration,ContractComplete,ContractDate,ContractActive,Created")] Contract contract)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contract).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerID = new SelectList(db.Owners, "OwnerID", "OwnerName", contract.OwnerID);
            return View(contract);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            return View(contract);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contract contract = db.Contracts.Find(id);
            db.Contracts.Remove(contract);
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

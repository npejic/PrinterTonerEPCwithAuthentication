using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PrinterTonerEPCwithAuthentication.Models;
using System.Data.Entity; //needed because of Include

namespace PrinterTonerEPCwithAuthentication.Controllers
{
    public class ContractsController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// All contracts
        /// </summary>
        /// <returns>list of contracts ordered by Owner name and Date</returns>
        public ActionResult Index()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var contracts = db.Contracts.Include(c => c.Owner).OrderByDescending(c => c.ContractDate);//.OrderBy(c => c.Owner.OwnerName).ThenBy(c => c.ContractDate);
                return View(contracts.ToList());
            }
        }

        /// <summary>
        /// Report No.2 for contracts that are no longer active
        /// </summary>
        /// <returns>sends inactiveOwners list to View</returns>
        public ActionResult InactiveOwners()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {

                var inactiveOwners = from i in db.Contracts select i;
                var Today = DateTime.Now.Date.AddMonths(-3); //.Date is not suported by LINQ

                inactiveOwners = inactiveOwners.Include(o => o.Owner).Where(a => DbFunctions.AddMonths(a.ContractDate, a.ContactDuration) < Today);

                return View(inactiveOwners.ToList());
            }
        }

        //TODO: ne koristi se
        public ActionResult Details(int? id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
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
        }

        //TODO: nije implementiran USING()
        public ActionResult Create()
        {
            ApplicationDbContext db = new ApplicationDbContext();


            var aaa = db.Owners.OrderBy(n=>n.OwnerName);
            ViewBag.OwnerID = new SelectList(aaa, "OwnerID", "OwnerName");
            return View();
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContractID,ContractName,OwnerID,ContractIs,ContactDuration,ContractComplete,ContractDate,ContractActive,ContractValid,Created")] Contract contract)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
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
        }

        //TODO: nije implementiran USING()
        public ActionResult Edit(int? id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
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
        public ActionResult Edit([Bind(Include = "ContractID,ContractName,OwnerID,ContractIs,ContactDuration,ContractComplete,ContractDate,ContractActive,ContractValid,Created")] Contract contract)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(contract).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                //ViewBag.OwnerID = new SelectList(db.Owners, "OwnerID", "OwnerName", contract.OwnerID);
                return View(contract);
            }
        }

        //TODO: nije implementiran USING()
        public ActionResult Delete(int? id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            
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
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Contract contract = db.Contracts.Find(id);
                db.Contracts.Remove(contract);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        //TODO: da li treba da se implementira USING()?
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

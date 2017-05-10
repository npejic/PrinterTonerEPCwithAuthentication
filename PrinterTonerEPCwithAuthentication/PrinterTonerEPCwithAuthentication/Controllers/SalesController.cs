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
    public class SalesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Sales
        public ActionResult Index()
        {
            var sales = db.Sales.Include(s => s.Contract).Include(s => s.Printer).OrderBy(s => s.Contract.ContractName).ThenBy(s => s.Contract.ContractDate);
            return View(sales.ToList());
        }

        public ActionResult SalesReportByOwner(string searchByOwner)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var sales = db.Sales.Where(s=>s.Printer.Owner.OwnerName=="EPC DOO").OrderBy(s => s.Contract.Owner.OwnerName).ThenBy(s => s.Contract.ContractName);

            if (!string.IsNullOrEmpty(searchByOwner))
            {
                sales = sales.Where(s => s.Contract.Owner.OwnerName.Contains(searchByOwner)).OrderBy(s => s.Contract.Owner.OwnerName).ThenBy(s => s.Contract.ContractName);// && s.printer.isepcprinter==true);
            }

            return View(sales.ToList());
        }

        /// <summary>
        /// Action Method using viewAsPdf class to create view as pdf ROTATIVA
        /// </summary>
        /// <returns></returns>
        public ActionResult DownloadPDF()
        {
            try
            {
                var model = new Sale();

                //get the information to display in pdf from database
                //for the time
                //Hard coding values are here, these are the content to display in pdf 
                var content = "<h2>WOW Rotativa<h2>" +
                 "<p>Ohh This is very easy to generate pdf using Rotativa <p>";
                var logoFile = @"/Images/logo.png";

                model.PDFContent = content;
                model.PDFLogo = Server.MapPath(logoFile);

                //Use ViewAsPdf Class to generate pdf using GeneratePDF.cshtml view
                return new Rotativa.ViewAsPdf("GeneratePDF", model) { FileName = "firstPdf.pdf" };
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        /// <summary>
        /// Action method to return view as pdf
        /// </summary>
        /// <returns></returns>
        public ActionResult GeneratePDF()
        {
            try
            {
                var model = new Sale();
                //Your data from db

                //hard coded value for test purpose
                var content = "<h2>PDF Created<h2>" +
                "<p>Ohh This is very easy to generate pdf using Rotativa<p>";
                var logoFile = @"/Images/logo.png";

                //model.PDFContent = content;
                //model.PDFLogo = Server.MapPath(logoFile);

                return View();
                //return View(model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        public ActionResult CreateByOwner()
        {
            ViewBag.PrinterID = new SelectList(db.Printers, "PrinterID", "PrinterSerialNo");

            return View();
        }

        public ActionResult Create()
        {
            var printersNotOwnedByEPC = db.Printers.Where(p => p.Owner.OwnerName == "EPC DOO").OrderBy(n=>n.PrinterSerialNo);
            var sortedContract = db.Contracts.OrderBy(n => n.ContractName);
            ViewBag.ContractID = new SelectList(sortedContract, "ContractID", "ContractName");
            ViewBag.PrinterID = new SelectList(printersNotOwnedByEPC, "PrinterID", "PrinterSerialNo");
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SaleID,SaleDate,Price,LocationOfPrinterIs,ContractID,AlternateContract,PrinterID,TonerID,Created,Remark")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                db.Sales.Add(sale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContractID = new SelectList(db.Contracts, "ContractID", "ContractName", sale.ContractID);
            ViewBag.PrinterID = new SelectList(db.Printers, "PrinterID", "PrinterInternalNo", sale.PrinterID);
            return View(sale);
        }

        public ActionResult EditSalesReportByOwner(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContractID = new SelectList(db.Contracts, "ContractID", "ContractName", sale.ContractID);
            ViewBag.PrinterID = new SelectList(db.Printers, "PrinterID", "PrinterInternalNo", sale.PrinterID);
            
            return View(sale);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSalesReportByOwner([Bind(Include = "SaleID,SaleDate,Price,LocationOfPrinterIs,ContractID,AlternateContract,PrinterID,TonerID,Created,Remark")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("SalesReportByOwner");
            }
            ViewBag.ContractID = new SelectList(db.Contracts, "ContractID", "ContractName", sale.ContractID);
            ViewBag.PrinterID = new SelectList(db.Printers, "PrinterID", "PrinterInternalNo", sale.PrinterID);
           
            return View(sale);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            var printersNotOwnedByEPC = db.Printers.Where(p => p.Owner.OwnerName == "EPC DOO").OrderBy(n => n.PrinterSerialNo);
            var sortedContract = db.Contracts.OrderBy(n => n.ContractName);
            ViewBag.ContractID = new SelectList(sortedContract, "ContractID", "ContractName", sale.ContractID);
            ViewBag.PrinterID = new SelectList(printersNotOwnedByEPC, "PrinterID", "PrinterInternalNo", sale.PrinterID);
            
            return View(sale);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SaleID,SaleDate,Price,LocationOfPrinterIs,ContractID,AlternateContract,PrinterID,TonerID,Created,Remark")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContractID = new SelectList(db.Contracts, "ContractID", "ContractName", sale.ContractID);
            ViewBag.PrinterID = new SelectList(db.Printers, "PrinterID", "PrinterInternalNo", sale.PrinterID);
            
            return View(sale);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sale sale = db.Sales.Find(id);
            db.Sales.Remove(sale);
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

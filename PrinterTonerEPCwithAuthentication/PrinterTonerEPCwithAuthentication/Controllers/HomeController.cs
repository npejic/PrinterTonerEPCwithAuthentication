using PrinterTonerEPCwithAuthentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity; //needed because of Include

namespace PrinterTonerEPCwithAuthentication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<ToDo> openedTasks = null;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {

                var sales = from s in db.Sales
                            where s.Printer.Owner.OwnerName == "EPC DOO"
                            select s;
                //Ukupan broj EPC štampača na iznajmljivanju
                var CountRentedPrinters = sales.Count();
                ViewData["CountRentedPrinters"] = CountRentedPrinters;

                TempData["treasurySumRSD"] = db.Treasuries.Sum(c => c.AmountRSD);
                TempData["treasurySumEUR"] = db.Treasuries.Sum(c => c.AmountEUR);

                //list of opened todoes (without closing date), shown on top of the HomeIndexView
                //Include is for EAGER loading of ApplicationUser
                openedTasks = db.ToDoes.Include(c=>c.ApplicationUser).Where(c => c.Closed == null).OrderBy(c => c.Created).ToList();
            }
                               
            
            return View(openedTasks);
        }

        public ActionResult Help()
        {
            //ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult DataEntry()
        {
            return View();
        }
        public ActionResult DataChange()
        {
            return View();
        }
    }
}
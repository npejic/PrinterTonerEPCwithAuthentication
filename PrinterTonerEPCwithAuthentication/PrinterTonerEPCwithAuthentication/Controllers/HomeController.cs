using PrinterTonerEPCwithAuthentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrinterTonerEPCwithAuthentication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //Izračunava ukupan broj EPC štampača na iznajmljivanju
            ApplicationDbContext db = new ApplicationDbContext();
            var sales = from s in db.Sales
                        where s.Printer.Owner.OwnerName == "EPC DOO"
                        select s;
            var CountRentedPrinters = sales.Count();
            ViewData["CountRentedPrinters"] = CountRentedPrinters;

            //list of opened todoes (without closing date), shown on top of the HomeIndexView
            //var openedTasks = db.ToDoes.Include(t => t.ApplicationUser).Where(c => c.Closed == null).OrderBy(c => c.Created).ToList();
            var openedTasks = db.ToDoes.Where(c => c.Closed == null).OrderBy(c => c.Created).ToList();
            return View(openedTasks);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using PrinterTonerEPCwithAuthentication.Models;
using System.Data.Entity;

namespace PrinterTonerEPCwithAuthentication.Common
{
    public class ControllerMethods
    {
        public static List<TonerTotal> DifferencesBetweenSoldAndMadeToners()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var soldToners = db.SaleToners.GroupBy(r => r.Toner.TonerModel).Select(r => new TonerTotal()
                {
                    TotalTonerModel = r.Key,
                    TonerTotalCount = r.Sum(c => c.TonerQuantity),
                }).OrderByDescending(c => c.TonerTotalCount).ToList();

                var madeToners = db.MakeToners.GroupBy(r => r.Toner.TonerModel).Select(r => new TonerTotal()
                {
                    TotalTonerModel = r.Key,
                    TonerTotalCount = r.Sum(c => c.MakeTonerQuantity),
                }).OrderByDescending(c => c.TonerTotalCount).ToList();

                var differences = from x1 in madeToners
                                  join x2 in soldToners on x1.TotalTonerModel equals x2.TotalTonerModel into temp
                                  join x3 in db.Toners on x1.TotalTonerModel equals x3.TonerModel
                                  from x2 in temp.DefaultIfEmpty()
                                  select new TonerTotal
                                  {
                                      TotalTonerModel = x1.TotalTonerModel,
                                      TonerTotalCount = x1.TonerTotalCount - (x2 != null ? x2.TonerTotalCount : 0),
                                      TonerTotalMin = x3.TonerMinQuantity
                                  };
                return differences.ToList();
            }
        }

        public static List<SaleToner> LastTonerSoldByOwnerOrTonerModel(string searchParameterOwner, string searchParameterTonerModel)
        {
            //TODO: prijavljuje grešku The ObjectContext instance has been disposed and can no longer be used for operations that require a connection
            //using (ApplicationDbContext db = new ApplicationDbContext())
            //{
            ApplicationDbContext db = new ApplicationDbContext();
                var lastTonerSale = db.SaleToners.Where(c => c.Owner.OwnerIsActive == true).GroupBy(g => new { g.Owner.OwnerName, g.TonerID })
                    .Select(s => s.OrderByDescending(x => x.SaleTonerDate).FirstOrDefault()).OrderBy(s => s.Owner.OwnerName).ThenBy(s => s.Toner.TonerModel);

                if (!String.IsNullOrEmpty(searchParameterOwner))
                {
                    lastTonerSale = lastTonerSale.Where(o => o.Owner.OwnerName.Contains(searchParameterOwner)).OrderBy(s => s.Owner.OwnerName).ThenBy(s => s.Toner.TonerModel).ThenBy(s => s.SaleTonerDate);
                }

                if (!String.IsNullOrEmpty(searchParameterTonerModel))
                {
                    lastTonerSale = lastTonerSale.Where(o => o.Toner.TonerModel.Contains(searchParameterTonerModel)).OrderBy(s => s.Owner.OwnerName).ThenBy(s => s.Toner.TonerModel).ThenBy(s => s.SaleTonerDate);
                }

                return lastTonerSale.ToList();
            //}
        }

        public static List<Owner> OwnersWithNoTonerOrder()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var ownersWithNoOrder = db.Owners
                                           .Where(c => !db.SaleToners
                                           .Select(b => b.OwnerID)
                                           .Contains(c.OwnerID)).ToList();
                return ownersWithNoOrder;
            }
        }

        public static List<SaleToner> OwnersWithNoTonerOrderInSomePeriod(string searchParameterPeriodInMonths)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var ownersWithNoAlarmOrder = db.SaleToners
                                                .Where(c => c.Owner.OwnerIsActive == true)
                                                .GroupBy(c => c.OwnerID)
                                                .Select(s => s.OrderByDescending(x => x.SaleTonerDate).FirstOrDefault()).OrderBy(c => c.SaleTonerDate);

                if (!String.IsNullOrEmpty(searchParameterPeriodInMonths))
                {
                    int period = Int16.Parse(searchParameterPeriodInMonths);
                    var LimitDate = DateTime.Now.Date;
                    LimitDate = LimitDate.AddMonths(-period);
                    ownersWithNoAlarmOrder = ownersWithNoAlarmOrder.Where(o => o.SaleTonerDate < LimitDate).OrderBy(s => s.SaleTonerDate);
                }
                return ownersWithNoAlarmOrder.ToList();
            }
        }

        public static List<TonerTotal> ListOfAllSoldTonersInPeriod(string dateFromString, string dateToString)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var soldToners = db.SaleToners.GroupBy(r => r.Toner.TonerModel).Select(r => new TonerTotal()
                {
                    TotalTonerModel = r.Key,
                    TonerTotalCount = r.Sum(c => c.TonerQuantity),
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
                return soldToners.ToList();
            }
        }

        public static int SumOfAllSoldTonersInPeriod(List<TonerTotal> soldToners)
        {
             //Izračunava ukupan broj prodatih tonera
            var CountSoldToners = soldToners.Sum(c => c.TonerTotalCount);
            return CountSoldToners;
            //ViewData["CountSoldToners"] = CountSoldToners;
            
        
        }

        public static List<SaleToner>  OrderedListOfSoldTonersFirstByDateAndThenByOwner()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var saleToners = db.SaleToners.Include(s => s.Owner).Include(s => s.Toner).OrderByDescending(s => s.SaleTonerDate).ThenBy(c => c.Owner.OwnerName);
                return saleToners.ToList();
            }
        }

        //public static void CreateSaleToners()
        //{
        //    using (ApplicationDbContext db = new ApplicationDbContext())
        //    {
        //        var saleToners = db.SaleToners.Include(s => s.Owner).Include(s => s.Toner).OrderByDescending(s => s.SaleTonerDate).ThenBy(c => c.Owner.OwnerName);
        //        return saleToners.ToList();
        //    }
        //}
    }
}
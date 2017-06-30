using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PrinterTonerEPCwithAuthentication.Models;

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

        public static List<SaleToner> LastTonerSoldByOwnerOrTonerModel(List<SaleToner> lastTonerSale, string searchParameter)
        {
            return lastTonerSale;
        }
    }
}
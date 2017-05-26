using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrinterTonerEPCwithAuthentication.Models
{
    public class Toner : BaseClass
    {
        public int TonerID { get; set; }
        
        [Required(ErrorMessage = "Morate uneti model tonera")]
        [Index(IsUnique = true)]
        [StringLength(100)]
        public string TonerModel { get; set; }
        public bool TonerIsOriginal { get; set; }
        public int? TonerYield { get; set; }
        public int? TonerGram { get; set; }
        public string TonerProductNo { get; set; }
        public int? TonerMinQuantity { get; set; }
        public string Remark { get; set; }

        public virtual ICollection<SaleToner> SaleToners { get; set; }
        public virtual ICollection<ComplaintToner> ComplaintToners { get; set; }
        public virtual ICollection<MakeToner> MakeToners { get; set; }
        public virtual ICollection<PrinterTonerCompatibility> PrinterTonerCompatibilitys { get; set; }
    }

    public class TonerTotal
    {
        public string TotalTonerModel { get; set; }
        public int TonerTotalCount { get; set; }
    }
}
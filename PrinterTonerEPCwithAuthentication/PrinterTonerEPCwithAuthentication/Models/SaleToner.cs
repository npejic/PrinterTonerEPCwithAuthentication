using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace PrinterTonerEPCwithAuthentication.Models
{
    public class SaleToner
    {
        public int SaleTonerID { get; set; }
        
        [Required(ErrorMessage = "Morate uneti datum prodaje YYYY.MM.DD")]
        public DateTime SaleTonerDate { get; set; }

        [Required(ErrorMessage = "Morate uneti cenu tonera")]
        public float TonerPrice { get; set; }
        public int TonerQuantity { get; set; }
        public string Remark { get; set; }

        public int OwnerID { get; set; }
        public virtual Owner Owner { get; set; }

        public int TonerID { get; set; }
        public virtual Toner Toner { get; set; }

        public string InvoiceNo { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrinterTonerEPCwithAuthentication.Models
{
    public class Sale : BaseClass
    {
        public int SaleID { get; set; }
        
        [Required(ErrorMessage = "Morate uneti datum prodaje YYYY.MM.DD")]
        public DateTime SaleDate { get; set; }
        public float Price { get; set; }

        public int ContractID { get; set; }
        public virtual Contract Contract { get; set; }

        public string AlternateContract { get; set; }
        public string Remark { get; set; }
        
        public int PrinterID { get; set; }
        public virtual Printer Printer { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrinterTonerEPCwithAuthentication.Models
{
    public class Servis : BaseClass
    {
        public int ServisID { get; set; }

        [Required(ErrorMessage = "Morate uneti datum servisa.")]
        public DateTime ServisDate { get; set; }
        
        public float ServisPrice { get; set; }

        public int PrinterID { get; set; }
        public virtual Printer Printer { get; set; }

        public string Remark { get; set; }
    }
}
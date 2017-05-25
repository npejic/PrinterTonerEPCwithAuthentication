using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace PrinterTonerEPCwithAuthentication.Models
{
    public class MakeToner
    {
        public int MakeTonerID { get; set; }

        [Required(ErrorMessage = "Morate uneti datum prodaje YYYY.MM.DD")]
        public DateTime MakeTonerDate { get; set; }

        public int MakeTonerQuantity { get; set; }
        public double MakeTonerPrice { get; set; }
        public string Remark { get; set; }

        public int TonerID { get; set; }
        public virtual Toner Toner { get; set; }

        public int OwnerID { get; set; }
        public virtual Owner Owner { get; set; }
    }
}
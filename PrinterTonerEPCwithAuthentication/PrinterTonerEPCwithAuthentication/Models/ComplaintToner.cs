using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrinterTonerEPCwithAuthentication.Models
{
    public class ComplaintToner : BaseClass
    {
        [Key]
        public int ComplaintTonerID { get; set; }

        [Required(ErrorMessage = "Morate uneti datum reklamacije.")]
        public DateTime ComplaintTonerDate { get; set; }

        public int TonerID { get; set; }
        public virtual Toner Toner { get; set; }

        public int ComplaintFromOwnerID { get; set; }
        public int ComplaintToOwnerID { get; set; }

        public virtual Owner ComplaintFromOwner { get; set; }
        public virtual Owner ComplaintToOwner { get; set; }

        public int ComplaintTonerQuantity { get; set; }
        public bool IsReady { get; set; }
        public string Remark { get; set; }
    }
}
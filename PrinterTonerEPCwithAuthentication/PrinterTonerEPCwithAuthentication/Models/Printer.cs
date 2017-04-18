using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrinterTonerEPCwithAuthentication.Models
{
    public class Printer : BaseClass
    {
        public int PrinterID { get; set; }

        [Required(ErrorMessage = "Morate uneti naziv interni broj štampača")]
        [Index(IsUnique = true)]
        [StringLength(100)]
        public string PrinterInternalNo { get; set; }
        
        [Required(ErrorMessage = "Morate uneti naziv proizvođača štampača")]
        [Index("Manufaturer_Model", 1, IsUnique = true)]
        [StringLength(100)]
        public string PrinterManufacturer { get; set; }
        
        [Required(ErrorMessage = "Morate uneti model štampača")]
        [Index("Manufaturer_Model", 2, IsUnique = true)]
        [StringLength(100)]
        public string PrinterModel { get; set; }
        
        [Required(ErrorMessage = "Morate uneti serijski broj štampača")]
        [StringLength(100)]
        [Index("Manufaturer_Model", 3, IsUnique = true)]
        [Index(IsUnique = true)]
        public string PrinterSerialNo { get; set; }

        public string PrinterHardwareNo { get; set; }
               
        public bool PrinterDecommissioned { get; set; }
        public string Remark { get; set; }

        public int? OwnerID { get; set; }
        public virtual Owner Owner { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
        public virtual ICollection<Servis> Servis { get; set; }

        public virtual ICollection<PrinterTonerCompatibility> PrinterTonerCompatibilitys { get; set; }
    }
}
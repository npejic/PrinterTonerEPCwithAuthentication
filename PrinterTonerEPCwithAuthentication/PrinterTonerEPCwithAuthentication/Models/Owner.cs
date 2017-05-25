using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrinterTonerEPCwithAuthentication.Models
{
    public class Owner : BaseClass
    {
        public int OwnerID { get; set; }
        
        [Index(IsUnique = true)]
        [Required(ErrorMessage = "Morate uneti naziv firme.")]
        [StringLength(100)]
        public string OwnerName { get; set; }
        
        [Required(ErrorMessage = "Morate uneti telefon firme.")]
        public string OwnerTelephone { get; set; }
        
        [Required(ErrorMessage = "Morate uneti adresu firme.")]
        public string OwnerAddress { get; set; }
        public string OwnerContact { get; set; }
        public string OwnerContactTelephone { get; set; }
        public string OwnerEmail { get; set; }
        
        [Required(ErrorMessage = "Morate uneti PIB firme.")]
        [Index(IsUnique = true)]
        [StringLength(100)]
        public string OwnerPIB { get; set; }
        
        [Index(IsUnique = true)]
        [StringLength(100)]
        [Required(ErrorMessage = "Morate uneti matični broj firme.")]
        public string OwnerMatBroj { get; set; }
       
        public bool OwnerIsInPDV { get; set; }
        public bool OwnerIsActive { get; set; }
        public string Remark { get; set; }

        public virtual ICollection<Printer> Printers { get; set; }
        
        public virtual ICollection<Contract> Contracts { get; set; }
        public virtual ICollection<Servis> Servis { get; set; }
        public virtual ICollection<Toner> Toners { get; set; }
        public virtual ICollection<MakeToner> MakeToners { get; set; }
        public virtual ICollection<ComplaintToner> ComplaintTonersFrom { get; set; }
        public virtual ICollection<ComplaintToner> ComplaintTonersTo { get; set; }
    }
}
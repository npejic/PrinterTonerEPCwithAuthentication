using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrinterTonerEPCwithAuthentication.Models
{
    public class Contract : BaseClass
    {
        [Key]
        public int ContractID { get; set; }
        
        [Required(ErrorMessage = "Morate uneti naziv ugovora.")]
        [StringLength(100)]
        [Index(IsUnique = true)]
        public string ContractName { get; set; }

        [Required(ErrorMessage = "Morate uneti datum ugovora.")]
        public DateTime ContractDate { get; set; }

        public int OwnerID { get; set; }
        public virtual Owner Owner { get; set; }

        [Required(ErrorMessage = "Morate uneti tip ugovora.")]
        public ContractType ContractIs { get; set; }
        public enum ContractType { Pausal, GratisRenta, Else }

        [Required(ErrorMessage = "Morate uneti trajanje ugovora.")]
        [Range(0, 36, ErrorMessage = "Trajanje ugovora se unosi u mesecima i mora biti veće od 0")]
        public int ContactDuration { get; set; }
         
        public bool ContractComplete { get; set; }
        public bool ContractValid { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }

        public string Remark { get; set; }
    }
}
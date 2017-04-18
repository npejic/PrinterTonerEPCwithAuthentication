using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrinterTonerEPCwithAuthentication.Models
{
    public class ToDo : BaseClass
    {
        public int ToDoID { get; set; }

        [Required(ErrorMessage = "Morate uneti opis posla")]
        public string Description { get; set; }

        //TODO: ubaci uvo u model
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Closed { get; set; }
        
        public int UserID { get; set; }
        public virtual User User { get; set; }

        public bool IsReady { get; set; }
        public string Remark { get; set; }
    }
}
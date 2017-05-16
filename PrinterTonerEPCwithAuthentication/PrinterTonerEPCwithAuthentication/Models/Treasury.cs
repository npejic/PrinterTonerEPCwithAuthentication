using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrinterTonerEPCwithAuthentication.Models
{
    public class Treasury : BaseClass
    {
        public Treasury()
        {
            this.ApplicationUser.Nick = HttpContext.Current.User.Identity.Name;
        }
        public int TreasuryID { get; set; }
        public string ApplicationUserID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public double Amount { get; set; }
        public enum TypeOfExpence { putarina, reprezentacija, ostalo}
        public TypeOfExpence Expence { get; set; }
        public string Remark { get; set; }
    }
}
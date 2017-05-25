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
            //TODO:
            //this.ApplicationUser.Nick = HttpContext.Current.User.Identity.Name;
        }
        public int TreasuryID { get; set; }
        public string ApplicationUserID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public double AmountRSD { get; set; }
        public double AmountEUR { get; set; }
        public enum TypeOfExpence { Banka, Gas, Gorivo, Kontakt_provizija, Ostalo, Oprema, Plata, Pošta, Pozajmica, Prazne_toner_kasete, Putni_troškovi, Reprezentacija, Rezervni_delovi, Servis_računara, Servis_štampača, Toner_kaseta }
        public TypeOfExpence Expence { get; set; }
        public string Remark { get; set; }
    }
}
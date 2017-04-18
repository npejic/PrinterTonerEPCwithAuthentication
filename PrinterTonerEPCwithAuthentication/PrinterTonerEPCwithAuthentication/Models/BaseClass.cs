using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrinterTonerEPCwithAuthentication.Models
{
    abstract public class BaseClass
    {
        public BaseClass()
        {
            string zoneId = "W. Europe Standard Time";
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById(zoneId);
            DateTime utcTime = DateTime.UtcNow;
            this.Created = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi);
            //this.Created = DateTime.Now;
        }
        /// <summary>
        /// Property that stores current date and time of creation
        /// </summary>
        public DateTime Created { get; set; }
    }
}
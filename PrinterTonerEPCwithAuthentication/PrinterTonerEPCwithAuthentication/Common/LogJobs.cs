using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PrinterTonerEPCwithAuthentication.Common
{
    public class LogJobs
    {
        public static void LogError(Exception ex)
        {
            string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            message += string.Format("Message: {0}", ex.Message);
            message += Environment.NewLine;
            message += string.Format("StackTrace: {0}", ex.StackTrace);
            message += Environment.NewLine;
            message += string.Format("Source: {0}", ex.Source);
            message += Environment.NewLine;
            message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            string path = System.Web.HttpContext.Current.Server.MapPath("~/logs/LogError.txt");
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(message);
                writer.Close();
            }
        }

        public static void LogSuccess(string currentJob, string currentController, string currentAction)
        {
            string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            var currentUser = System.Web.HttpContext.Current.User.Identity.Name;
            message += Environment.NewLine;
            message += "Job ID: " + currentJob;
            message += Environment.NewLine;
            message += "Controller; " + currentController;
            message += Environment.NewLine;
            message += "Action:" + currentAction;
            message += Environment.NewLine;
            message += "User:" + currentUser;
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            string path = System.Web.HttpContext.Current.Server.MapPath("~/logs/Log.txt");  
            using (StreamWriter writer = new StreamWriter(path, true))                      
            {
                writer.WriteLine(message);
                writer.Close();
            }
        }
    }
}
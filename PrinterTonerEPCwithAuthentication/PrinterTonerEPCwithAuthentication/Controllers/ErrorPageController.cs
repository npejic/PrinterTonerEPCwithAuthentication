﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrinterTonerEPCwithAuthentication.Controllers
{
    [Authorize(Roles = "user")]
    public class ErrorPageController : Controller
    {
        // GET: ErrorPage
        public ActionResult ErrorMessage()
        {
            return View();
        }
    }
}
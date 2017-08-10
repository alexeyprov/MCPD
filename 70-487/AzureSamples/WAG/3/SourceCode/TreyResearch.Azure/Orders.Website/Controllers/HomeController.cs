//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace Orders.Website.Controllers
{
    using System.Web.Mvc;
    using Orders.Website.CustomAttributes;

    [LogAction]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Trey Research";

            return View();
        }
    }
}

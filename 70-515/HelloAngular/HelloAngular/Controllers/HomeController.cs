using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HelloAngular.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ShoppingCart()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult AMail()
        {
            return View();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//這裡可以寫一些用共用的method
namespace OOADProject.Controllers
{
    public class SharedController : Controller
    {
        // GET: Shared
        public ActionResult Index()
        {
            return View();
        }
    }
}
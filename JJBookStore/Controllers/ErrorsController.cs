using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JJBookStore.Controllers
{
    public class ErrorsController : Controller
    {
        // GET: Errors
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult BadRequest()
        {
            return View();
        }
    }
}
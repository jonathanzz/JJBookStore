using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace JJBookStoreAdmin.Controllers
{

    public class HomeController : Controller
    {
        private BookStoreEntities db = new BookStoreEntities();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            var ad = db.Admins.FirstOrDefault(a => a.AdminName == admin.AdminName);
            if (ad != null)
            {
                if (ad.AmPassword == admin.AmPassword)
                {
                    FormsAuthentication.SetAuthCookie(ad.AdminName, false);
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, ad.AdminID.ToString(), DateTime.Now, 
                        DateTime.Now.AddMinutes(30), false, ad.AmPassword, FormsAuthentication.FormsCookiePath);
                    string encTicket = FormsAuthentication.Encrypt(ticket);

                    // Create the cookie.
                    Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

                    // Redirect back to original URL.
                    //Response.Redirect(FormsAuthentication.GetRedirectUrl(ad.AdminName, false));
                   return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Incorrect password.");
            }
            else
            {
                ModelState.AddModelError("", "Incorrect admin name.");
            }
            return View();
        }
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login");
        }
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";


            return View();
        }
        public ActionResult Test()
        {
            return View();
        }
    }



}

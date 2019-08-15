using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JJBookStore.DAL;
using JJBookStore.Models;
using System.Web.Security;
using JJBookStore.ViewModels;
using System.Data.Entity.Validation;

namespace JJBookStore.Controllers
{
    public class UsersController : Controller
    {
        private BookStoreContext db = new BookStoreContext();

        // GET: Users/SignIn
        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }

        // POST: Users/SignIn
        [HttpPost]
        public ActionResult SignIn(SignInViewModel s)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.FirstOrDefault(u => (u.UserName == s.SignInName || u.EmailAddress == s.SignInName)
                                                        && u.Password == s.Password);
                if (user != null)
                {
                    if (user.IsValid)
                    {
                        FormsAuthentication.SetAuthCookie(user.UserName, false);
                        Session["UserID"] = user.UserID;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                        ModelState.AddModelError("", "Your account is invalid now, please valid it through your email link.");
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect username or password, please try again.");
                }
            }
            return View();
        }
        // GET: Users/Details
        //Directly get user ID from session, to avoid id shows on URL
        public async Task<ActionResult> Details()
        {
            if (Session["UserID"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int id = Convert.ToInt32(Session["UserID"]);
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Users/Register
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel r)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.FirstOrDefault(u => u.UserName == r.UserName || u.EmailAddress == r.EmailAddress);
                if (user != null)
                {
                    ModelState.AddModelError("", "Username or Email address has been registered, please sign in or change another");
                    return View();
                }

                db.Users.Add(new User
                {
                    UserName = r.UserName,
                    Password = r.Password,
                    EmailAddress = r.EmailAddress,
                    BirthDate = r.BirthDate,
                    FirstName = r.FirstName,
                    LastName = r.LastName,
                    Address = r.Address,
                    IsValid = true   //Ready to be changed to false after validation function finished. 
                });
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // GET: Users/Edit
        public async Task<ActionResult> Edit()
        {
            if (Session["UserID"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int id = Convert.ToInt32(Session["UserID"]);
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            var e = new EditUserViewModel
            {
                UserID = user.UserID,
                Address = user.Address,
                EmailAddress = user.EmailAddress,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate
            };
            return View(e);
        }

        // POST: Users/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditUserViewModel e)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FindAsync(e.UserID);
                if (user == null)
                    return HttpNotFound();
                user.EmailAddress = e.EmailAddress;
                user.BirthDate = e.BirthDate;
                user.FirstName = e.FirstName;
                user.LastName = e.LastName;
                user.Address = e.Address;
                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Details");
            }
            return View();
        }

        // GET: Users/ChangPwd
        public async Task<ActionResult> ChangePwd()
        {
            if (Session["UserID"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int id = Convert.ToInt32(Session["UserID"]);
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(new ChangePwdViewModel { UserID = id });
        }

        // POST: Users/ChangePwd
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePwd(ChangePwdViewModel c)
        {
            if (ModelState.IsValid)
            {

                User user = await db.Users.FindAsync(c.UserID);
                if (user == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                if (user.Password != c.CurrentPwd)
                {
                    ModelState.AddModelError("", "Current password is incorrect");
                    return View(c);
                }
                user.Password = c.Password;
                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        //GET: Users/SignOut
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

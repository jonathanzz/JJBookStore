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
using JJBookStore.Utility;

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
                        //   FormsAuthentication.SetAuthCookie(user.UserName, false);
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2, user.UserName, DateTime.Now,
                        DateTime.Now.AddMinutes(30), s.RememberMe, user.UserID.ToString(), FormsAuthentication.FormsCookiePath);
                        string encTicket = FormsAuthentication.Encrypt(ticket);
                        // Create the cookie.
                        Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
                        //Temp data to store welcome message as an alert window
                        TempData["Msg"] = "alert('Welcome Back " + user.UserName + " !')";
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
        [Authorize]
        public async Task<ActionResult> Details()
        {
            FormsIdentity userIDIdentity = (FormsIdentity)User.Identity;
            int id = Convert.ToInt32(userIDIdentity.Ticket.UserData);
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
                var newUser = new User();
                db.Users.Add(RegisterViewModel.ConvertToUser(r, newUser));
                await db.SaveChangesAsync();
                if (EmailUtil.RegisterConfirmation(newUser))
                {
                    TempData["Msg"] = "alert('Congratulations! You have been registered successfully! " +
                        "Please click the validation link in your Email box to validate your account.')";
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }
                return RedirectToAction("SignIn", "Users");
            }

            return View();
        }

        //GET: Users/NewUserValidation/id=?&&validString=?
        public async Task<ActionResult> NewUserValidation(int id, string validateString)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            if (!MD5Util.Decrypt(validateString).Equals(user.UserID.ToString()+user.UserName + user.EmailAddress))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (user.IsValid == true)
            {
                return View(false);
            }
            user.IsValid = true;
            db.Entry(user).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return View(true);
        }


        // GET: Users/Edit
        [Authorize]
        public async Task<ActionResult> Edit()
        {
            FormsIdentity userIDIdentity = (FormsIdentity)User.Identity;
            int id = Convert.ToInt32(userIDIdentity.Ticket.UserData);
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(new EditUserViewModel(user));
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
                if (!e.EmailAddress.Equals(user.EmailAddress))
                {
                    var checkUser = db.Users.FirstOrDefault(u => u.EmailAddress == e.EmailAddress);
                    if (checkUser != null)
                    {
                        ModelState.AddModelError("", "Sorry! This Email address has been registered, please try another one");
                        return View();
                    }
                }
                user = EditUserViewModel.ConvertToUser(e, user);
                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();
                TempData["Msg"] = "alert('Your profile has been updated successfully!')";
                return RedirectToAction("Details");
            }
            return View();
        }

        // GET: Users/ChangPwd
        [Authorize]
        public async Task<ActionResult> ChangePwd()
        {
            FormsIdentity userIDIdentity = (FormsIdentity)User.Identity;
            int id = Convert.ToInt32(userIDIdentity.Ticket.UserData);
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
                FormsAuthentication.SignOut();
                TempData["Msg"] = "alert('Your password has been changed successfully! Please sign in again.')";
                return RedirectToAction("SignIn");
            }
            return View();
        }

        //GET: Users/SignOut
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("SignIn");
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

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
    public class ShopCartsController : Controller
    {
        private BookStoreContext db = new BookStoreContext();


        //Get: ShopCarts/ListCart
        public ActionResult ListCart()
        {
            if (Session["UserID"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int UserID = Convert.ToInt32(Session["UserID"]);
            var shopCarts = from s in db.ShopCarts where s.UserID == UserID select s;
            List<ShopCartViewModel> shopCartsList = new List<ShopCartViewModel>();
            foreach (var s in shopCarts)
            {
                shopCartsList.Add(new ShopCartViewModel
                {
                    ShopCartId = s.ShopCartId,
                    BookID = s.BookID,
                    Title = s.Book.Title,
                    Seller = s.Book.User.UserName,
                    Amount = s.Amount,
                    Price = s.Book.Price
                });
            }
            return View(shopCartsList);
        }

        // GET: ShopCarts/AddToCart
        public async Task<ActionResult> AddToCart(int bookID)
        {
            if (Session["UserID"] == null || bookID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int userID = Convert.ToInt32(Session["UserID"]);
            var sc = db.ShopCarts.FirstOrDefault(s => s.UserID == userID && s.BookID == bookID);
            if (sc != null)
            {
                sc.Amount++;
                db.Entry(sc).State = EntityState.Modified;
            }
            else
            {
                db.ShopCarts.Add(new ShopCart
                {
                    BookID = (int)bookID,
                    UserID = userID,
                    Amount = 1
                });
            }
            await db.SaveChangesAsync();
            return RedirectToAction("ListCart", "ShopCarts");
        }
    }
}
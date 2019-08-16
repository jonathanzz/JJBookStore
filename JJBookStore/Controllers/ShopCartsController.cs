using JJBookStore.DAL;
using JJBookStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace JJBookStore.Controllers
{
    public class ShopCartsController : Controller
    {
        private BookStoreContext db = new BookStoreContext();
        //GET: ShopCarts/ViewShopCart
        public async Task<ActionResult> ViewShopCart()
        {
            if (Session["UserID"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int id = Convert.ToInt32(Session["UserID"]);
            var shopCartList = db.ShopCarts.Where(sc => sc.UserID == id);
            return View(await shopCartList.ToListAsync());
        }
        //GET: ShopCarts/AddToShopCart/5
        public async Task<ActionResult> AddToShopCart(int id)
        {
            if (Session["UserID"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int UserID = Convert.ToInt32(Session["UserID"]);
            var shopCart = db.ShopCarts.FirstOrDefault(sc => sc.UserID == UserID && sc.BookID == id);
            if (shopCart != null)
            {
                shopCart.Amount++;
                db.Entry(shopCart).State = EntityState.Modified;
            }
            else
            {
                db.ShopCarts.Add(new ShopCart
                {
                    UserID = UserID,
                    BookID = id,
                    Amount = 1
                });
            }
            await db.SaveChangesAsync();
            TempData["Msg"] = "alert('This book has been added to shopping cart successfully!')";
            return RedirectToAction("ViewShopCart");
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShopCart shopCart = await db.ShopCarts.FindAsync(id);
            if (shopCart == null)
            {
                return HttpNotFound();
            }
            db.ShopCarts.Remove(shopCart);
            await db.SaveChangesAsync();
            return RedirectToAction("SellingBook");
        }
    }
}
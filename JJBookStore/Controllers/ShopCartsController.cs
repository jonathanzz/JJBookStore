using JJBookStore.DAL;
using JJBookStore.Models;
using JJBookStore.ViewModels;
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
            var shopCartVMList = shopCartList.Select(sc => new ShopCartViewModel
            {
                ShopCartId = sc.ShopCartId,
                BookID = sc.BookID,
                Title = sc.Book.Title,
                Seller = sc.User.UserName,
                Quantity = sc.Quantity,
                UnitPrice = sc.Book.Price
            }).ToListAsync();
            return View(await shopCartVMList);
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
                shopCart.Quantity++;
                db.Entry(shopCart).State = EntityState.Modified;
            }
            else
            {
                db.ShopCarts.Add(new ShopCart
                {
                    UserID = UserID,
                    BookID = id,
                    Quantity = 1,
                    CreatedTime = DateTime.Now
                });
            }
            await db.SaveChangesAsync();
            TempData["Msg"] = "alert('This book has been added to shopping cart successfully!')";
            return RedirectToAction("ViewShopCart");
        }
        //POST: ShopCarts/SaveShopCart
        public async Task<ActionResult> SaveShopCart(IEnumerable<ShopCartViewModel> scList)
        {
            if (ModelState.IsValid)
            {
                foreach(var sc in scList)
                {
                    var shopcart = await db.ShopCarts.FindAsync(sc.ShopCartId);
                    if(shopcart==null)
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    shopcart.Quantity = sc.Quantity;
                    db.Entry(shopcart).State = EntityState.Modified;
                }
                await db.SaveChangesAsync();
                return RedirectToAction("ViewShopCart");
            }
            return View();
        }

        //GET: ShopCarts/Remove/5
        public async Task<ActionResult> Remove(int? id)
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
            return RedirectToAction("ViewShopCart");
        }
    }
}
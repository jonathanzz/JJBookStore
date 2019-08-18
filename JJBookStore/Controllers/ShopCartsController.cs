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
        public ActionResult ViewShopCart()
        {
            if (Session["UserID"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int id = Convert.ToInt32(Session["UserID"]);
            var shopCartList = db.ShopCarts.Where(sc => sc.UserID == id);
            var scList = new ShopCartViewModelList();
            foreach (var sc in shopCartList)
            {
                scList.scList.Add(new ShopCartViewModel(sc));
            }
            return View(scList);
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

        public Task<ActionResult> ShopCartAction(string shopCartButton, ShopCartViewModelList scVMList)
        {
            switch (shopCartButton)
            {
                case "Save Change":
                    // delegate sending to another controller action
                    return (SaveShopCart(scVMList));
                case "Remove":
                    // call another action to perform the cancellation
                    return (RemoveShopCart(scVMList));
                default:
                    // If they've submitted the form without a submitButton, 
                    // just return the view again.
                    return null;
            }
        }
        //POST: ShopCarts/SaveShopCart
        private async Task<ActionResult> SaveShopCart(ShopCartViewModelList scVMList)
        {

            if (ModelState.IsValid)
            {
                foreach (var sc in scVMList.scList)
                {
                    if (sc.check)
                    {
                        var shopcart = await db.ShopCarts.FindAsync(sc.ShopCartId);
                        if (shopcart == null)
                            return HttpNotFound();
                        shopcart.Quantity = sc.Quantity;
                        db.Entry(shopcart).State = EntityState.Modified;
                    }
                }
                await db.SaveChangesAsync();
                TempData["Msg"] = "alert('Item selected has been saved successfully!')";
                return RedirectToAction("ViewShopCart");
            }
            return View();
        }

        //POST: ShopCarts/Remove/5
        private async Task<ActionResult> RemoveShopCart(ShopCartViewModelList scVMList)
        {

            if (ModelState.IsValid)
            {
                foreach (var sc in scVMList.scList)
                {
                    if (sc.check)
                    {
                        var shopcart = await db.ShopCarts.FindAsync(sc.ShopCartId);
                        if (shopcart == null)
                            return HttpNotFound();
                        db.ShopCarts.Remove(shopcart);
                    }
                }
                await db.SaveChangesAsync();
                TempData["Msg"] = "alert('Item selected has been removed successfully!')";
                return RedirectToAction("ViewShopCart");
            }
            return View();
        }
    }
}
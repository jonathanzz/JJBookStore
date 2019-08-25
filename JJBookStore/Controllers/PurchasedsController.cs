using JJBookStore.DAL;
using JJBookStore.Models;
using JJBookStore.Utility;
using JJBookStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace JJBookStore.Controllers
{
    [Authorize]
    public class PurchasedsController : Controller
    {

        private BookStoreContext db = new BookStoreContext();
        // GET: Purchased
        public async Task<ActionResult> PurchaseIndex()
        {
            FormsIdentity userIDIdentity = (FormsIdentity)User.Identity;
            int id = Convert.ToInt32(userIDIdentity.Ticket.UserData);
            var purchaseds = from p in db.Purchaseds where p.UserID == id select p;
            return View(await purchaseds.ToListAsync());
        }

        public ActionResult PaymentConfirm()
        {
            return View();
        }

        public ActionResult PurchaseCancel(int? bookID)
        {
            if (bookID == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var book = db.Books.Find(bookID);
            if (book == null)
                return HttpNotFound();
            ViewBag.Title = book.Title;
            ViewBag.Seller = book.User.UserName;
            ViewBag.Email = book.User.EmailAddress;
            return View();
        }
        public async Task<ActionResult> PurchaseNow(ShopCartViewModelList scVMList)
        {

            if (ModelState.IsValid)
            {
                foreach (var scVm in scVMList.scList)
                {
                    if (scVm.check)
                    {

                        var shopcart = await db.ShopCarts.FindAsync(scVm.ShopCartId);
                        if (shopcart == null)
                            return HttpNotFound();
                        var book = await db.Books.FindAsync(shopcart.BookID);
                        if (book == null)
                            return HttpNotFound();
                        //Detect book StockQty, if not enough then cancel this purchase
                        if (book.StockQty < scVm.Quantity)
                        {
                            return RedirectToAction("PurchaseCancel", "Purchases", book.BookID);
                        }
                        else if (book.StockQty == scVm.Quantity)
                        {
                            //Send Email to tell seller that one item is out of stock.
                            if (SendEmail.OutofStockNotification(book))
                            {
                                book.OnSell = false;
                            }
                            else
                            {
                                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                            }
                        }

                        book.StockQty -= 1;                                               //Modified book StockQty if it was sold
                        db.Entry(book).State = EntityState.Modified;
                        db.Purchaseds.Add(new Purchased(shopcart, scVm.Quantity));
                        db.ShopCarts.Remove(shopcart);                                  //Remove purchased items from shopcart
                        if (!SendEmail.SoldNotification(scVm, book))
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                        }
                    }
                }
                await db.SaveChangesAsync();
                TempData["Msg"] = "alert('Item selected has been purchased successfully!')";
                return RedirectToAction("PurchaseIndex", "Purchaseds");
            }
            return View();
        }
    }
}
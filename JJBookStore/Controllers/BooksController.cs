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
using System.Linq.Dynamic;
using JJBookStore.ViewModels;

namespace JJBookStore.Controllers
{
    public class BooksController : Controller
    {
        private BookStoreContext db = new BookStoreContext();

        // GET: Books/Search
        public async Task<ActionResult> Search(string searchString, string columnString)
        {
            //Dynamic Linq to query different column dynamically
            var books = db.Books.Where(columnString + ".Contains" + "(\"" + searchString + "\")");
            return View(await books.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/
        public async Task<ActionResult> SellingBook()
        {
            if (Session["UserID"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int sellserID = Convert.ToInt32(Session["UserID"]);
            var books = from b in db.Books where b.UserID == sellserID select b;
            return View(await books.ToListAsync());
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateBookViewModel c)
        {
            if (ModelState.IsValid)
            {
                if (Session["UserID"] == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                int id = Convert.ToInt32(Session["UserID"]);
                var book = new Book
                {
                    UserID = id,
                    Title = c.Title,
                    Author = c.Author,
                    Description = c.Description,
                    Amount = c.Amount,
                    Img = c.Img,
                    Price = c.Price,
                    UploadDate = c.UploadDate,
                    OnSell = c.OnSell
                };
                db.Books.Add(book);
                await db.SaveChangesAsync();
                return RedirectToAction("SellingBook");
            }
            return View();
        }

        // GET: Books/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            var e = new EditBookViewModel
            {
                BookID = book.BookID,
                Title = book.Title,
                Author = book.Author,
                Description = book.Description,
                Amount = book.Amount,
                Price = book.Price,
                Img = book.Img,
                UploadDate = book.UploadDate,
                OnSell = book.OnSell
            };
            return View(e);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditBookViewModel e)
        {
            if (ModelState.IsValid)
            {
                Book book = await db.Books.FindAsync(e.BookID);
                if (book == null)
                    return HttpNotFound();
                book.Title = e.Title;
                book.Author = e.Author;
                book.Description = e.Description;
                book.Amount = e.Amount;
                book.Price = e.Price;
                book.Img = e.Img;
                book.UploadDate = e.UploadDate;
                book.OnSell = e.OnSell;
                db.Entry(book).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(e);
        }

        // GET: Books/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Book book = await db.Books.FindAsync(id);
            db.Books.Remove(book);
            await db.SaveChangesAsync();
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

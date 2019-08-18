﻿using System;
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
using PagedList;

namespace JJBookStore.Controllers
{
    public class BooksController : Controller
    {
        private BookStoreContext db = new BookStoreContext();
        private static int PageSize = 5; //one page contains elements

        // GET: Books/Search
        public  ActionResult Search(string searchString, string columnString,string currentSearch, int? page)
        {
            ViewBag.columnString = columnString;
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentSearch;
            }

            ViewBag.currentSearch = searchString;
            //Dynamic Linq to query different column dynamically
            var books = db.Books.Where(columnString + ".Contains" + "(\"" + searchString + "\")").OrderByDescending(b => b.Title);
            //pageable
            ViewBag.totalResult = books.Count();
            int pageNumber = (page ?? 1);
            return View(books.ToPagedList(pageNumber, PageSize));
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
                var book = new Book();
                db.Books.Add(CreateBookViewModel.ConvertToBook(c,book,id));
                await db.SaveChangesAsync();
                TempData["Msg"] = "alert('Book: " + c.Title+" has been created successfully!')";
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
            return View(new EditBookViewModel(book));
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
                db.Entry(EditBookViewModel.ConvertToBook(e,book)).State = EntityState.Modified;
                await db.SaveChangesAsync();
                TempData["Msg"] = "alert('Book: " + e.Title + " has been edited successfully!')";
                return RedirectToAction("SellingBook");
            }
            return View(e);
        }

        // GET: Books/Remove/5
        public async Task<ActionResult> Remove(int? id)
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
            db.Books.Remove(book);
            await db.SaveChangesAsync();
            return RedirectToAction("SellingBook");
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

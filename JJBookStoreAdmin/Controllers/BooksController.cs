﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using JJBookStoreAdmin;

namespace JJBookStoreAdmin.Controllers
{
    public class BooksController : Controller
    {
        private BookStoreEntities db = new BookStoreEntities();

        public ActionResult Index()
        {
            PopulateUsers();
            return View();
        }

        private void PopulateUsers()
        {
            var users = db.Users.ToList();
            ViewData["users"] = users;
            ViewData["defaultUser"] = users.First();
        }

        public ActionResult Books_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<Book> books = db.Books;
            DataSourceResult result = books.ToDataSourceResult(request, book => new {
                UserID = book.UserID,
                BookID = book.BookID,
                Title = book.Title,
                Author = book.Author,
                Description = book.Description,
                Price = book.Price,
                Img = book.Img,
                UploadDate = book.UploadDate,
                OnSell = book.OnSell,
                StockQty = book.StockQty,
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Books_Create([DataSourceRequest]DataSourceRequest request, Book book)
        {
            if (ModelState.IsValid)
            {
                var entity = new Book
                {
                    UserID = book.UserID,
                    Title = book.Title,
                    Author = book.Author,
                    Description = book.Description,
                    Price = book.Price,
                    Img = book.Img,
                    UploadDate = book.UploadDate,
                    OnSell = book.OnSell,
                    StockQty = book.StockQty,
                };

                db.Books.Add(entity);
                db.SaveChanges();
                book.BookID = entity.BookID;
            }

            return Json(new[] { book }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Books_Update([DataSourceRequest]DataSourceRequest request, Book book)
        {
            if (ModelState.IsValid)
            {
                var entity = new Book
                {
                    UserID = book.UserID,
                    BookID = book.BookID,
                    Title = book.Title,
                    Author = book.Author,
                    Description = book.Description,
                    Price = book.Price,
                    Img = book.Img,
                    UploadDate = book.UploadDate,
                    OnSell = book.OnSell,
                    StockQty = book.StockQty,
                };

                db.Books.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { book }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Books_Destroy([DataSourceRequest]DataSourceRequest request, Book book)
        {
            if (ModelState.IsValid)
            {
                var entity = new Book
                {
                    UserID=book.UserID,
                    BookID = book.BookID,
                    Title = book.Title,
                    Author = book.Author,
                    Description = book.Description,
                    Price = book.Price,
                    Img = book.Img,
                    UploadDate = book.UploadDate,
                    OnSell = book.OnSell,
                    StockQty = book.StockQty,
                };

                db.Books.Attach(entity);
                db.Books.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { book }.ToDataSourceResult(request, ModelState));
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}

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
    public class ShopCartsController : Controller
    {
        private BookStoreEntities db = new BookStoreEntities();

        public ActionResult Index()
        {
            PopulateUsers();
            PopulateBooks();
            return View();
        }
        private void PopulateUsers()
        {
            var users = db.Users.ToList();
            ViewData["users"] = users;
            ViewData["defaultUser"] = users.First();
        }
        private void PopulateBooks()
        {
            var books = db.Books.ToList();
            ViewData["books"] = books;
            ViewData["defaultUser"] = books.First();
        }
        public ActionResult ShopCarts_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<ShopCart> shopcarts = db.ShopCarts;
            DataSourceResult result = shopcarts.ToDataSourceResult(request, shopCart => new
            {
                BookID = shopCart.BookID,
                UserID = shopCart.UserID,
                ShopCartId = shopCart.ShopCartId,
                Quantity = shopCart.Quantity,
                CreatedTime = shopCart.CreatedTime,
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ShopCarts_Create([DataSourceRequest]DataSourceRequest request, ShopCart shopCart)
        {
            if (ModelState.IsValid)
            {
                var entity = new ShopCart
                {
                    BookID = shopCart.BookID,
                    UserID = shopCart.UserID,
                    Quantity = shopCart.Quantity,
                    CreatedTime = shopCart.CreatedTime
                };

                db.ShopCarts.Add(entity);
                db.SaveChanges();
                shopCart.ShopCartId = entity.ShopCartId;
            }

            return Json(new[] { shopCart }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ShopCarts_Update([DataSourceRequest]DataSourceRequest request, ShopCart shopCart)
        {
            if (ModelState.IsValid)
            {
                var entity = new ShopCart
                {
                    BookID = shopCart.BookID,
                    UserID = shopCart.UserID,
                    ShopCartId = shopCart.ShopCartId,
                    Quantity = shopCart.Quantity,
                    CreatedTime = shopCart.CreatedTime,
                };

                db.ShopCarts.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { shopCart }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ShopCarts_Destroy([DataSourceRequest]DataSourceRequest request, ShopCart shopCart)
        {
            if (ModelState.IsValid)
            {
                var entity = new ShopCart
                {
                    BookID = shopCart.BookID,
                    UserID = shopCart.UserID,
                    ShopCartId = shopCart.ShopCartId,
                    Quantity = shopCart.Quantity,
                    CreatedTime = shopCart.CreatedTime,
                };

                db.ShopCarts.Attach(entity);
                db.ShopCarts.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { shopCart }.ToDataSourceResult(request, ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}

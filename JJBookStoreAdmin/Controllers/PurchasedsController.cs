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
    public class PurchasedsController : Controller
    {
        private BookStoreEntities db = new BookStoreEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Purchaseds_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<Purchased> purchaseds = db.Purchaseds;
            DataSourceResult result = purchaseds.ToDataSourceResult(request, purchased => new {
                PurchasedID = purchased.PurchasedID,
                Title = purchased.Title,
                Quantity = purchased.Quantity,
                PurchasedPrice = purchased.PurchasedPrice,
                PurchasedTime = purchased.PurchasedTime,
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Purchaseds_Create([DataSourceRequest]DataSourceRequest request, Purchased purchased)
        {
            if (ModelState.IsValid)
            {
                var entity = new Purchased
                {
                    Title = purchased.Title,
                    Quantity = purchased.Quantity,
                    PurchasedPrice = purchased.PurchasedPrice,
                    PurchasedTime = purchased.PurchasedTime,
                };

                db.Purchaseds.Add(entity);
                db.SaveChanges();
                purchased.PurchasedID = entity.PurchasedID;
            }

            return Json(new[] { purchased }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Purchaseds_Update([DataSourceRequest]DataSourceRequest request, Purchased purchased)
        {
            if (ModelState.IsValid)
            {
                var entity = new Purchased
                {
                    PurchasedID = purchased.PurchasedID,
                    Title = purchased.Title,
                    Quantity = purchased.Quantity,
                    PurchasedPrice = purchased.PurchasedPrice,
                    PurchasedTime = purchased.PurchasedTime,
                };

                db.Purchaseds.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { purchased }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Purchaseds_Destroy([DataSourceRequest]DataSourceRequest request, Purchased purchased)
        {
            if (ModelState.IsValid)
            {
                var entity = new Purchased
                {
                    PurchasedID = purchased.PurchasedID,
                    Title = purchased.Title,
                    Quantity = purchased.Quantity,
                    PurchasedPrice = purchased.PurchasedPrice,
                    PurchasedTime = purchased.PurchasedTime,
                };

                db.Purchaseds.Attach(entity);
                db.Purchaseds.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { purchased }.ToDataSourceResult(request, ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}

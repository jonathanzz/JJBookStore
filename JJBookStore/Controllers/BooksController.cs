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
using System.Web.Security;
using System.IO;

namespace JJBookStore.Controllers
{
    public class BooksController : Controller
    {
        private BookStoreContext db = new BookStoreContext();
        private static int PageSize = 5; //one page contains elements
        // GET: Books/Search
        public ActionResult Search(string searchString, string columnString, string currentSearch, int? page)
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
            var books = db.Books.Where(columnString + ".Contains" + "(\"" + searchString + "\")").Where(b => b.OnSell == true).OrderByDescending(b => b.Title);
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

        // GET: Books/SellingBook
        [Authorize]
        public async Task<ActionResult> SellingBook()
        {
            FormsIdentity userIDIdentity = (FormsIdentity)User.Identity;
            int id = Convert.ToInt32(userIDIdentity.Ticket.UserData);
            var books = from b in db.Books where b.UserID == id select b;
            return View(await books.ToListAsync());
        }

        //GET: Books/SaleRecord/5
        [Authorize]
        public ActionResult SaleRecord(int? id)
        {
            var purchaseds = db.Purchaseds.Where(p => p.BookID == id);
            IList<SaleRecordViewModel> saleRecordList = new List<SaleRecordViewModel>();
            foreach (var p in purchaseds)
            {
                saleRecordList.Add(new SaleRecordViewModel(p));
            }
            return View(saleRecordList);
        }

        // GET: Books/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Create(CreateBookViewModel c)
        {
            if (ModelState.IsValid)
            {
                var imgUrl = UploadImage(c.Img, "");
                if (!String.IsNullOrEmpty(imgUrl))
                {
                    FormsIdentity userIDIdentity = (FormsIdentity)User.Identity;
                    int id = Convert.ToInt32(userIDIdentity.Ticket.UserData);
                    var book = new Book();
                    db.Books.Add(CreateBookViewModel.ConvertToBook(c, book, id, imgUrl));
                    await db.SaveChangesAsync();
                    TempData["Msg"] = "alert('Book: " + c.Title + " has been created successfully!')";
                    return RedirectToAction("SellingBook");
                }
                else
                {
                    ModelState.AddModelError("", "Please choose either a GIF, JPG or PNG image.");
                }
            }
            return View();
        }

        // GET: Books/Edit/5
        [Authorize]
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
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditBookViewModel e)
        {
            if (ModelState.IsValid)
            {
                var imgUrl = UploadImage(e.Img, e.OriginalImgUrl);
                if (!String.IsNullOrEmpty(imgUrl))
                {
                    Book book = await db.Books.FindAsync(e.BookID);
                    if (book == null)
                        return HttpNotFound();
                    db.Entry(EditBookViewModel.ConvertToBook(e, book, imgUrl)).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    TempData["Msg"] = "alert('Book: " + e.Title + " has been edited successfully!')";
                    return RedirectToAction("SellingBook");
                }
                else
                {
                    ModelState.AddModelError("", "Please choose either a GIF, JPG or PNG image.");
                }
            }
            return View(e);
        }

        // GET: Books/Remove/5
        [Authorize]
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

        private string UploadImage(HttpPostedFileBase img, string originalImgUrl)
        {
            var uploadDir = "~/Uploads/BookImg/";
            var defulatImg = "defaultImg.jpg";
            var imageUrl = "";
            var validImageTypes = new string[]
            {
                "image/gif",
                "image/jpeg",
                "image/pjpeg",
                "image/png"
            };
            if (img == null || img.ContentLength == 0)
            {
                if (String.IsNullOrEmpty(originalImgUrl))
                {
                    imageUrl = Path.Combine(uploadDir, defulatImg);
                }
                else
                {
                    imageUrl = originalImgUrl;
                }
            }
            else if (validImageTypes.Contains(img.ContentType))
            {
                var imagePath = Path.Combine(Server.MapPath(uploadDir), img.FileName);
                imageUrl = Path.Combine(uploadDir, img.FileName);
                img.SaveAs(imagePath);
                if (!String.IsNullOrEmpty(originalImgUrl) 
                    && !originalImgUrl.Equals(Path.Combine(uploadDir, defulatImg)))
                {
                    var originalPath = Path.Combine(Server.MapPath(originalImgUrl));
                    System.IO.File.Delete(originalPath);
                }
            }

            return imageUrl;
        }
    }
}

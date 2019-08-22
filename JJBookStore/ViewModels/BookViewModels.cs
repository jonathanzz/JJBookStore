using JJBookStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JJBookStore.ViewModels
{
    public class CreateBookViewModel
    {

        public int UserID { get; set; }
        [Required(ErrorMessage = "Book title can not be empty")]
        [StringLength(100, ErrorMessage = "Title cannot be longer than 50 characters.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Stock Quantity can not be empty")]
        [Display(Name = "Stock Quantity")]
        public int StockQty { get; set; }
        [StringLength(50, ErrorMessage = "Author Name cannot be longer than 50 characters.")]
        public string Author { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Price can not be empty")]
        public double Price { get; set; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase Img { get; set; }
        [DataType(DataType.Date)]
        public DateTime? UploadDate { get; set; }
        public bool OnSell { get; set; }

        public CreateBookViewModel() { }

        public static Book ConvertToBook(CreateBookViewModel c, Book book, int id, string imgUrl)
        {
            book.UserID = id;
            book.Title = c.Title;
            book.Author = c.Author;
            book.Description = c.Description;
            book.StockQty = c.StockQty;
            book.Img = imgUrl;
            book.Price = c.Price;
            book.UploadDate = c.UploadDate;
            book.OnSell = c.OnSell;
            return book;
        }
    }

    public class EditBookViewModel
    {
        public int BookID { get; set; }
        [Required(ErrorMessage = "Book title can not be empty")]
        [StringLength(100, ErrorMessage = "Title cannot be longer than 50 characters.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Stock Quantity can not be empty")]
        [Display(Name = "Stock Quantity")]
        public int StockQty { get; set; }
        [StringLength(50, ErrorMessage = "Author Name cannot be longer than 50 characters.")]
        public string Author { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Price can not be empty")]
        public double Price { get; set; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase Img { get; set; }
        public string OriginalImgUrl { get; set; }
        [DataType(DataType.Date)]
        public DateTime? UploadDate { get; set; }
        public bool OnSell { get; set; }

        public EditBookViewModel() { }

        public EditBookViewModel(Book book)
        {
            BookID = book.BookID;
            Title = book.Title;
            Author = book.Author;
            Description = book.Description;
            StockQty = book.StockQty;
            Price = book.Price;
            Img = null;
            OriginalImgUrl = book.Img;
            UploadDate = book.UploadDate;
            OnSell = book.OnSell;
        }

        public static Book ConvertToBook(EditBookViewModel e, Book book, string imgUrl)
        {
            book.Title = e.Title;
            book.Author = e.Author;
            book.Description = e.Description;
            book.StockQty = e.StockQty;
            book.Price = e.Price;
            book.Img = imgUrl;
            book.UploadDate = e.UploadDate;
            book.OnSell = e.OnSell;
            return book;
        }
    }

   

}
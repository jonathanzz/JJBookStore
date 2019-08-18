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
        [Required(ErrorMessage = "Amount can not be empty")]
        public int Amount { get; set; }
        [StringLength(50, ErrorMessage = "Author Name cannot be longer than 50 characters.")]
        public string Author { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Price can not be empty")]
        public double Price { get; set; }
        [DataType(DataType.ImageUrl)]
        public string Img { get; set; }
        [DataType(DataType.Date)]
        public DateTime? UploadDate { get; set; }
        public bool OnSell { get; set; }

        public CreateBookViewModel() { }

        public static Book ConvertToBook(CreateBookViewModel c, Book book, int id)
        {
            book.UserID = id;
            book.Title = c.Title;
            book.Author = c.Author;
            book.Description = c.Description;
            book.Amount = c.Amount;
            book.Img = c.Img;
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
        [Required(ErrorMessage = "Amount can not be empty")]
        public int Amount { get; set; }
        [StringLength(50, ErrorMessage = "Author Name cannot be longer than 50 characters.")]
        public string Author { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Price can not be empty")]
        public double Price { get; set; }
        [DataType(DataType.ImageUrl)]
        public string Img { get; set; }
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
            Amount = book.Amount;
            Price = book.Price;
            Img = book.Img;
            UploadDate = book.UploadDate;
            OnSell = book.OnSell;
        }

        public static Book ConvertToBook(EditBookViewModel e, Book book)
        {
            book.Title = e.Title;
            book.Author = e.Author;
            book.Description = e.Description;
            book.Amount = e.Amount;
            book.Price = e.Price;
            book.Img = e.Img;
            book.UploadDate = e.UploadDate;
            book.OnSell = e.OnSell;
            return book;
        }
    }

}
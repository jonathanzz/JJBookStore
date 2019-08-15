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
    }
    
}
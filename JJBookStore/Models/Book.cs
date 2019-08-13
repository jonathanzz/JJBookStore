using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JJBookStore.Models
{
    public class Book
    {
        [Key]
        public int BookID { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Author Name cannot be longer than 100 characters.")]
        public string Title { get; set; }
        public int? UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual User User { get; set; }
        public int Amount { get; set; }
        [StringLength(50, ErrorMessage = "Author Name cannot be longer than 50 characters.")]
        public string Author { get; set; }
        public double price { get; set; }
        public string img { get; set; }
        public DateTime? uploadDate { get; set; }
        public bool OnSell { get; set; }
    }
}
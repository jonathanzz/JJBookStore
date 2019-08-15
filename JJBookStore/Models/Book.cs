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
        [StringLength(100)]
        public string Title { get; set; }
        public int? UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual User User { get; set; }
        public int Amount { get; set; }
        [StringLength(50)]
        public string Author { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Img { get; set; }
        public DateTime? UploadDate { get; set; }
        public bool OnSell { get; set; }
    }
}
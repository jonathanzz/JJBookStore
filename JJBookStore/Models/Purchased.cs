using JJBookStore.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JJBookStore.Models
{
    public class Purchased
    {   [Key]
        public int PurchasedID { get; set; }

        public string Title { get; set; }

        public int Quantity { get; set; }

        public double PurchasedPrice { get; set; }

        public int BookID { get; set; }
        [ForeignKey("BookID")]
        public virtual Book Book { get; set; }
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual User User { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime PurchasedTime { get; set; }

        public Purchased() {  }

        public Purchased(ShopCart sc, int q)
        {

            Title = sc.Book.Title;
            Quantity = q;
            PurchasedPrice = sc.Book.Price;
            UserID = sc.UserID;
            BookID = sc.BookID;
            PurchasedTime = DateTime.Now;
        }
    }
}
using JJBookStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JJBookStore.ViewModels
{
    public class SaleRecordViewModel
    {
        public string Title { get; set; }
        public string Buyer { get; set; }
        public int Quentity { get; set; }
        [Display(Name = "Unit Price")]
        public double Price { get; set; }
        [Display(Name = "Purchased Time")]
        [DataType(DataType.DateTime)]
        public DateTime PurchaseTime { get; set; }

        public SaleRecordViewModel() { }

        public SaleRecordViewModel(Purchased purchase)
        {
            Title = purchase.Book.Title;
            Buyer = purchase.User.UserName;
            Quentity = purchase.Quantity;
            Price = purchase.PurchasedPrice;
            PurchaseTime = purchase.PurchasedTime;
        }
    }
    
}
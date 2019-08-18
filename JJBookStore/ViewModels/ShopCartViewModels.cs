using JJBookStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JJBookStore.ViewModels
{
    public class ShopCartViewModel
    {
        public int ShopCartId { get; set; }
        public int BookID { get; set; }
        public string Title { get; set; }
        public string Seller { get; set; }
        [Required]
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }

        public bool check { get; set; }

        public ShopCartViewModel() { }

        public ShopCartViewModel(ShopCart sc)
        {
            ShopCartId = sc.ShopCartId;
            BookID = sc.BookID;
            Title = sc.Book.Title;
            Seller = sc.User.UserName;
            Quantity = sc.Quantity;
            UnitPrice = sc.Book.Price;
            check = false;
        }
    }

    public class ShopCartViewModelList
    {
        public IList<ShopCartViewModel> scList { get; set; }

        public ShopCartViewModelList()
        {
            scList = new List<ShopCartViewModel>();
        }
    }

}
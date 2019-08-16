using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JJBookStore.ViewModels
{
    public class ShoppingCartViewModel
    {
        public int ShopCartId { get; set; }
        public int BookID { get; set; }
        public string Title { get; set; }
        public string Seller { get; set; }
        public int Amount { get; set; }
    }
}
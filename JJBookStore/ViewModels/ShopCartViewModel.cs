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
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JJBookStore.Models
{
    public class ShopCart
    {
        [Key]
        public int ShopCartId { get; set; }
        public int BookID { get; set; }
        [ForeignKey("BookID")]
        public virtual Book Book { get; set; }
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual User User { get; set; }
        public int Quantity { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedTime { get; set; }

    }
}
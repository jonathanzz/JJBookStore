//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JJBookStoreAdmin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ShopCart
    {
        public int ShopCartId { get; set; }
        public int BookID { get; set; }
        public int UserID { get; set; }
        public int Quantity { get; set; }
        [DataType(DataType.DateTime)]
        public System.DateTime CreatedTime { get; set; }
    
        public virtual Book Book { get; set; }
        public virtual User User { get; set; }
    }
}

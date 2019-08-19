using JJBookStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace JJBookStore.DAL
{
    public class BookStoreContext : DbContext
    {
    
        public BookStoreContext() : base("name=BookStoreContext")
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Book> Books { get; set; }
        public DbSet<ShopCart> ShopCarts { get; set; }
        public DbSet<Purchased> Purchaseds { get; set; }
        public DbSet<Admin> Admins { get; set; }
    }
}

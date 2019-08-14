namespace JJBookStore.Migrations
{
    using JJBookStore.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<JJBookStore.DAL.BookStoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(JJBookStore.DAL.BookStoreContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            /*      var users = new System.Collections.Generic.List<User>
                  {
                  new User{UserName="zhaozhe1005",FirstName="Jonathan",LastName="Zhao", BirthDate=DateTime.Parse("05-10-1990"),
                      EmailAddress ="jzjonathanzz@gmail.com",Password="123",IsValid=true},
                  new User{UserName="elaine0791",FirstName="Elaine",LastName="Zhang", BirthDate=DateTime.Parse("08-07-1991"),
                      EmailAddress ="elainezhang0791@gmail.com",Password="321",IsValid=true},
                  };
                  users.ForEach(u => context.Users.Add(u));
                  context.Admins.Add(new Admin {AdminName="admin", AmPassword="1005" });

                  var books = new System.Collections.Generic.List<Book>
                  {
                      new Book{Title="book 1", Amount=99, OnSell=true, Author="author1", UserID=1, price=10, uploadDate=DateTime.Today},
                      new Book{Title="book 2", Amount=9, OnSell=true, Author="author2", UserID=1, price=20, uploadDate=DateTime.Today},
                      new Book{Title="book 3", Amount=9, OnSell=false, Author="author3", UserID=1, price=15, uploadDate=DateTime.Today}
                  };
                  books.ForEach(u => context.Books.Add(u));*/
        }
    }
}

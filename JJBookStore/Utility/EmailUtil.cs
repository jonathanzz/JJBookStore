using JJBookStore.Models;
using JJBookStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace JJBookStore.Utility
{
    public class EmailUtil
    {
        private static string SenderEmailAddress = "zhaozhe2632026@gmail.com";
        private static string SenderPassword = "zhaozhe1005";
        public static bool RegisterConfirmation(User user)
        {
            var validateString = HttpUtility.UrlEncode(MD5Util.Encrypt(user.UserID.ToString() + user.UserName + user.EmailAddress));
            var validateLink = "http://localhost:1234/Users/NewUserValidation?id=" +
                user.UserID.ToString() + "&validateString=" + validateString;
            var body = "<div style='text-align:center'><h3>Thanks For Your Registering! </h3> <p><b>" +
                "Thank you for registering in JJ Bookstore. Now you are a member of our JJ community." +
                " Please click the below link to activate your account:</b></p><a href='" + validateLink + "'>" + validateLink +
                "</a></br></br></br> <p style='font-size:8px'>This is a system generated email sent to" + user.UserName +
                ". Please do not reply. You're receiving this email because you're a member of the JJ Bookstore. " +
                "Don't miss out on exclusive offers </p></div>";
            var message = new MailMessage();
            message.To.Add(new MailAddress(user.EmailAddress));
            message.From = new MailAddress(SenderEmailAddress);
            message.Subject = "JJ Book Store: Register Confirmation";
            message.Body = body;
            message.IsBodyHtml = true;
            if (!SendMail(message))
                return false;
            return true;
        }

        public static bool SoldNotification(ShopCartViewModel scVM, Book book)
        {
            var body = "<div style='text-align:center'><h3>Book Selling Notification </h3> <p><b>" +
                "Congratulations! " + scVM.Quantity.ToString() + " of book ABCDEFG has been sold for " +
                (scVM.Quantity * scVM.UnitPrice).ToString() + "! please check the details in your account.</br>" +
                "</br></br> <p style='font-size:8px'>This is a system generated email sent to " + book.User.UserName +
                ". Please do not reply. You're receiving this email because you're a member of the JJ Bookstore. " +
                "Don't miss out on exclusive offers </p></div>";
            var message = new MailMessage();
            message.To.Add(new MailAddress(book.User.EmailAddress));
            message.From = new MailAddress(SenderEmailAddress);
            message.Subject = "JJ Book Store: Sold nofitication";
            message.Body = body;
            message.IsBodyHtml = true;
            if (!SendMail(message))
                return false;
            return true;
        }

        public static bool OutofStockNotification(Book book)
        {
            var body = "<div style='text-align:center'><h3>Book Selling Notification </h3> <p><b>" +
               "Attention! Your selling book " + book.Title + " at JJ Bookstore is out of stock now, and " +
               "it has been removed from sale list. Please manully edit your book detail if it still on selling." +
               "</br></br> <p style='font-size:8px'>This is a system generated email sent to " + book.User.UserName +
               ". Please do not reply. You're receiving this email because you're a member of the JJ Bookstore. " +
               "Don't miss out on exclusive offers </p></div>";
            var message = new MailMessage();
            message.To.Add(new MailAddress(book.User.EmailAddress));
            message.From = new MailAddress(SenderEmailAddress);
            message.Subject = "JJ Book Store:Out of stock notification";
            message.Body = body;
            message.IsBodyHtml = true;
            if (!SendMail(message))
                return false;
            return true;
        }

        private static bool SendMail(MailMessage mailMessage)
        {
            try
            {
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(SenderEmailAddress, SenderPassword)
                };
                //Don't need to send while development period.
                //smtp.Send(mailMessage);

                //Export to html file to see layout.
                var path = HttpContext.Current.Server.MapPath("~/Content/EmailBodyTest.html");
                System.IO.File.WriteAllText(path, mailMessage.Body);

            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

    }
}
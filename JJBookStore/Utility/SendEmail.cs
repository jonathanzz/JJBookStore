using JJBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace JJBookStore.Utility
{
    public class SendEmail
    {
        private static string SenderEmailAddress = "zhaozhe2632026@gmail.com";
        private static string SenderPassword = "zhaozhe1005";
        public static bool RegisterConfirmation(User user)
        {
            var body = "<p>Email From: JJ Book Store(NoReply)</p><p>Message:</p><p>this is a test email</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress(user.EmailAddress));  // replace with valid value 
            message.From = new MailAddress(SenderEmailAddress);  // replace with valid value
            message.Subject = "JJ Book Store: Register Confirmation";
            message.Body = body;
            message.IsBodyHtml = true;
            if (!SendMail(message))
                return false;
            return true;
        }

        public static bool SoldNotification(Book book)
        {
            var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress(book.User.EmailAddress));  // replace with valid value 
            message.From = new MailAddress(SenderEmailAddress);  // replace with valid value
            message.Subject = "JJ Book Store: Sold nofitication";
            message.Body = body;
            message.IsBodyHtml = true;
            if (!SendMail(message))
                return false;
            return true;
        }

        public static bool OutofStockNotification(Book book)
        {
            var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress(book.User.EmailAddress));  // replace with valid value 
            message.From = new MailAddress(SenderEmailAddress);  // replace with valid value
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
                System.IO.File.WriteAllText("EmailBodyTest.html", mailMessage.Body);

            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        
    }
}
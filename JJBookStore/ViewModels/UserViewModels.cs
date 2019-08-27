using JJBookStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JJBookStore.ViewModels
{
    public class SignInViewModel
    {
        [Required(ErrorMessage = "Username or Email address can not be empty")]
        [Display(Name = "Username or Email Address")]
        public string SignInName { get; set; }
        [Required(ErrorMessage = "Password can not be empty")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password should be least 6 characters.")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Username can not be empty")]
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password can not be empty")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password should be least 6 characters.")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
        [Required(ErrorMessage = "Email can not be empty")]
        [Display(Name = "Email Address: ")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateTime? BirthDate { get; set; }
        [StringLength(30, ErrorMessage = "First name cannot be longer than 30 characters.")]
        public string FirstName { get; set; }
        [StringLength(30, ErrorMessage = "Last name cannot be longer than 30 characters.")]
        public string LastName { get; set; }
        public string Address { get; set; }

        public RegisterViewModel()
        {

        }

        public static User ConvertToUser(RegisterViewModel r, User user)
        {
            user.UserName = r.UserName;
            user.Password = r.Password;
            user.EmailAddress = r.EmailAddress;
            user.BirthDate = r.BirthDate;
            user.FirstName = r.FirstName;
            user.LastName = r.LastName;
            user.Address = r.Address;
            user.IsValid = false;
            return user;
        }
    }

    public class EditUserViewModel
    {
        public int UserID { get; set; }
        [Required(ErrorMessage = "Email can not be empty")]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
        [StringLength(30, ErrorMessage = "First name cannot be longer than 30 characters.")]
        public string FirstName { get; set; }
        [StringLength(30, ErrorMessage = "Last name cannot be longer than 30 characters.")]
        public string LastName { get; set; }
        public string Address { get; set; }

        public EditUserViewModel() { }
        public EditUserViewModel(User user)
        {
            UserID = user.UserID;
            Address = user.Address;
            EmailAddress = user.EmailAddress;
            FirstName = user.FirstName;
            LastName = user.LastName;
            BirthDate = user.BirthDate;
        }
        public static User ConvertToUser(EditUserViewModel e, User user)
        {
            user.EmailAddress = e.EmailAddress;
            user.BirthDate = e.BirthDate;
            user.FirstName = e.FirstName;
            user.LastName = e.LastName;
            user.Address = e.Address;
            return user;
        }
    }

    public class ChangePwdViewModel
    {
        public int UserID { get; set; }
        [Required(ErrorMessage = "Current Password can not be empty")]
        [Display(Name = "Current password")]
        [DataType(DataType.Password)]
        public string CurrentPwd { get; set; }
        [Required(ErrorMessage = "New Password can not be empty")]
        [Display(Name = "New password")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password should be least 6 characters.")]
        public string Password { get; set; }
        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string PasswordConfirm { get; set; }
    }
}
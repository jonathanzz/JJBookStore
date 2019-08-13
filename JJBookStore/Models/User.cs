using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JJBookStore.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [StringLength(20, ErrorMessage = "Nickname cannot be longer than 20 characters.")]
        public string NickName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "First name cannot be longer than 30 characters.")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Last name cannot be longer than 30 characters.")]
        public string LastName { get; set; }
        public string Address { get; set; }
        public bool IsValid { get; set; }
    }
}
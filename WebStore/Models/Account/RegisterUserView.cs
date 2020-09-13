using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL;

namespace WebStore.Models.Account
{
    public class RegisterUserView
    {
        [Required, MaxLength(256), Display(Name = "User name")]
        public string UserName { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare(nameof(Password)), Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }
    }
}

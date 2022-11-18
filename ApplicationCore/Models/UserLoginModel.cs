using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class UserLoginModel
    {
        [Required (ErrorMessage="Email should not be empty")]
        [EmailAddress(ErrorMessage="Email should be in right format")]
        [StringLength(50, ErrorMessage= "Email cannot exceed 50 characters")]
        public string Email { get; set; }
        [Required (ErrorMessage="Password should not be empty")]
        //We don't need to use RegularExpression for the Password in Login Model,
        // because user already create strong password in the Register
        public string Password { get; set; }

    }
}
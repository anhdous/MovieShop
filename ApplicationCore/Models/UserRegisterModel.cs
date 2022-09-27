using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Validators;

namespace ApplicationCore.Models
{
    public class UserRegisterModel
    { 
        //Buit-in validator
        [Required (ErrorMessage="Email should not be empty")]
        [EmailAddress(ErrorMessage="Email should be in right format")]
        [StringLength(50, ErrorMessage= "Email cannot exceed 50 characters")]
        public string Email { get; set; }
        
        [Required (ErrorMessage="Password should not be empty")]
        [RegularExpression("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[#$^%+=!()@&]).{8,}$", ErrorMessage = "Password should have minimum 8 character with at least one upper, lower, number and special character")]
        public string Password { get; set; }
        
        [Required (ErrorMessage="First Name should not be empty")]
        [StringLength(50, ErrorMessage= "First Name cannot exceed 50 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name should not be empty")]
        [StringLength(50, ErrorMessage = "Last Name cannot exceed 50 characters")]
        public string LastName { get; set; }
        

        [Required (ErrorMessage="Date of Birth should not be empty")]
        // year should not be less than 1900
        // Minimum age should be 15
        [MinimumYearAllowed]
        public DateTime DateOfBirth { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Day12_JWT_Authenticate_Authorize.Data
{
    public class UserRegister
    {
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "DoB is Required")]
        [DataType(DataType.Date)]
        public DateTime Dob { get; set; }

        [Required(ErrorMessage = "DoJ is Required")]
        [DataType(DataType.Date)]
        public DateTime DoJ { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        [MinLength(8)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$", ErrorMessage = "Password must have number, Special Character, LowerCase, Upper Case")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Conform Password is Required")]
        [Compare("Password", ErrorMessage = "Password and Conform Password does not match")]
        public string ConformPassword { get; set; }

        [Required(ErrorMessage = "Gender is Required")]
        public string Gender { get; set; }
    }
}

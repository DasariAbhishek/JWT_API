using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Day12_JWT_Authenticate_Authorize.Data
{
    public class UserLogin
    {
        [Required(ErrorMessage = "UserId is Required")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Day12_JWT_Authenticate_Authorize.Models
{
    [Table("User")]
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime Dob { get; set; }

        [DataType(DataType.Date)]
        public DateTime DoJ { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Gender { get; set; }
        public string Role { get; set; }
    }
}

using Day12_JWT_Authenticate_Authorize.Data;
using Day12_JWT_Authenticate_Authorize.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Day12_JWT_Authenticate_Authorize.Repository
{
    public class UserService : IUserService
    {
        private readonly EmployeeDbContext _context;
        private readonly IConfiguration _configuration;
        //private readonly ILogger _log;

        public UserService(EmployeeDbContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
           // _log = log;
        }

        #region "Generate JWT Token"
        public string GenerateJWT(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            var credintials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            Claim[] claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Gender, user.Gender),
                new Claim(ClaimTypes.DateOfBirth, user.Dob.ToString()),
                //new Claim(ClaimTypes., user.DoJ.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var securityToken = new JwtSecurityToken(_configuration["JWT:issuer"],
                                                    _configuration["JWT:audience"],
                                                    claims,
                                                    expires: DateTime.Now.AddMinutes(1500),
                                                    signingCredentials: credintials);
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
        #endregion

        #region "Check User"
        public User CheckUser(UserLogin login)
        {
            try
            {
                var password = EncryptPassword(login.Password);
                if (login != null)
                    return _context.Users.FirstOrDefault(u => u.Email == login.UserEmail && u.Password == password);
            }
            catch (Exception exc)
            {
                //return StatusCode(StatusCodes.Status500InternalServerError, $"Server error {exc.Message}");
            }
            return null;

        }
        #endregion

        #region "Check Email"
        public bool CheckEmail(UserRegister reg)
        {
            try
            {
                if (reg != null)
                    return _context.Users.FirstOrDefault(u => u.Email == reg.Email) == null;
            }
            catch (Exception exc)
            {
                //return StatusCode(StatusCodes.Status500InternalServerError, $"Server error {exc.Message}");
            }
            return false;

        }
        #endregion

        #region Add User"
        public User AddUser(UserRegister reg)
        {
            User user = new User
            {
                Name = reg.Name,
                Email = reg.Email,
                Dob = reg.Dob,
                DoJ = reg.DoJ,
                Password = EncryptPassword(reg.Password),
                Gender = reg.Gender,
                Role = "User"
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }
        #endregion

        #region "Encrypt Password" //New Add
        public string EncryptPassword(string password)
        {
            MD5 md5 = MD5.Create();
            var enc = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(enc);
        }
        #endregion
    }
}

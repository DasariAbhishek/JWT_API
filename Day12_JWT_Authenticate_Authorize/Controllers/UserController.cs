using Day12_JWT_Authenticate_Authorize.Data;
using Day12_JWT_Authenticate_Authorize.Models;
using Day12_JWT_Authenticate_Authorize.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day12_JWT_Authenticate_Authorize.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        #region "Login Functionality"
        [HttpPost]
        [Route("Login")]
        public ActionResult Login([FromBody] UserLogin login)
        {
            if (ModelState.IsValid)
            {
                User user = _service.CheckUser(login);
                if (user == null)
                    return BadRequest(new { jwt = "", UserId = -1, msg = "User Not Found..." });

                var token = _service.GenerateJWT(user);
                return Ok(new { jwt = token, userId = user.UserId, role = user.Role, msg = "Successfully Logged In" });
            }
            return ValidationProblem("Fill the data Properly...");

        }
        #endregion

        #region "Register Functionality"
        [HttpPost]
        [Route("Register")]
        public  ActionResult Register([FromBody] UserRegister reg)
        {
            if (ModelState.IsValid)
            {
                var date = reg.Dob;

                if (date.AddYears(18) > DateTime.Now.Date)
                {
                    return BadRequest(new { msg = "You need to be more than 18 years Old" });
                }
                if (_service.CheckEmail(reg))
                {
                    User user = _service.AddUser(reg);

                    return CreatedAtAction("Register", user);
                }
                else
                {
                    return Conflict(new { msg = "Email Already Exists..." });
                }
            }
            else
            {
                return ValidationProblem("Fill the data Properly...");
                //return ValidationProblem(ModelState);
            }
        }
        #endregion
    }
}

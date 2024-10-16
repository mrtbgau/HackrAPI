using API.Models;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IJWTService JWTService, IConfiguration config, DbAPIContext dbAPIContext) : ControllerBase
    {
        private readonly IJWTService _JWTService = JWTService;
        private readonly IConfiguration _config = config;

        private readonly DbAPIContext _dbAPIcontext = dbAPIContext;

        // [HttpPost]
        // [Route("login")]
        // public IActionResult Login([FromBody] LoginModel model)
        // {
        //     var user = _JWTService.Login(model.UserName, model.Password);
        //     if (user != null)
        //     {
        //         var claims = new List<Claim>
        //         {
        //             new(ClaimTypes.Email, user.UserName),
        //         };
        //         var token = _JWTService.GenerateToken(_config["JWT:Key"], claims);
        //         return Ok(token);
        //     }
        //     return Unauthorized();
        // }

        [HttpPost]
        [Route("register")]
        public ActionResult<User> Register(string username, string pwd){
            var hmac = new HMACSHA512();
            var newUser = new User{
                UserName = username,
                UserPWD = hmac.ComputeHash(Encoding.UTF8.GetBytes(pwd))
            };

            _dbAPIcontext.Add(newUser);
            _dbAPIcontext.SaveChanges();

            return newUser;
        }
    }
}
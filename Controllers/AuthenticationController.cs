 using API.DTO;
using API.Models;
using API.Services.JWT;
using API.Services.Logs;
using Microsoft.AspNetCore.Cors;
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
    public class AuthenticationController(IJWTService JWTService, IConfiguration config, DbAPIContext dbAPIContext, ILogService logService) : ControllerBase
    {
        private readonly IJWTService _JWTService = JWTService;
        private readonly IConfiguration _config = config;
        private readonly ILogService _logService = logService;

        private readonly DbAPIContext _dbAPIcontext = dbAPIContext;

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginDTO loginDTO)
        {

            var user = _JWTService.Login(loginDTO);
        
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                    new(ClaimTypes.Email, user.mail!),
                    new(ClaimTypes.Name, user.UserName!),
                    new(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User")
                };

                _logService.LogAction(user.UserID, $"{user.UserName} s'est connecté");
            
                return Ok(new UserDTO
                {
                    UserID = user.UserID,
                    UserName = user.UserName!,
                    Mail = user.mail!,
                    IsAdmin = user.IsAdmin,
                    Token = _JWTService.GenerateToken(_config["JWT:Key"]!, claims)
                });
            }
        
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public ActionResult<UserDTO> Register(RegisterDTO registerDTO){
            if (_dbAPIcontext.Users.Any(u => u.mail == registerDTO.Mail))
                return BadRequest("Email already exists");

            if (_dbAPIcontext.Users.Any(u => u.UserName == registerDTO.UserName))
                return BadRequest("Username already exists");

            using var hmac = new HMACSHA512();
            var newUser = new User
            {
                UserName = registerDTO.UserName,
                mail = registerDTO.Mail,
                UserPWD = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password!)),
                PasswordSalt = hmac.Key,
                IsAdmin = false
            };

            _dbAPIcontext.Add(newUser);
            _dbAPIcontext.SaveChanges();

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, newUser.UserID.ToString()),
                new(ClaimTypes.Email, newUser.mail!),
                new(ClaimTypes.Name, newUser.UserName!),
                new(ClaimTypes.Role, "User")
            };

            _logService.LogAction(newUser.UserID, $"{newUser.UserName} s'est inscrit");

            return Ok(new UserDTO
            {
                UserID = newUser.UserID,
                UserName = newUser.UserName!,
                Mail = newUser.mail!,
                IsAdmin = newUser.IsAdmin,
                Token = _JWTService.GenerateToken(_config["JWT:Key"]!, claims)
            });
        }
    }
}
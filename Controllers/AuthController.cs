using API.Models;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IJWTService JWTService, IConfiguration config) : ControllerBase
    {
        private readonly IJWTService _JWTService = JWTService;
        private readonly IConfiguration _config = config;

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            var user = _JWTService.Login(model.UserName, model.Password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.UserName),
                };
                var token = _JWTService.GenerateToken(_config["JWT:Key"], claims);
                return Ok(token);
            }
            return Unauthorized();
        }
    }
}
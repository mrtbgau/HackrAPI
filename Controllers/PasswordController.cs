﻿using API.Services.Logs;
using API.Services.Password;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PasswordController(IPasswordService passwordService, ILogService logService) : Controller
    {
        private readonly IPasswordService _passwordService = passwordService;
        private readonly ILogService _logService = logService;

        [HttpGet("generate-password")]
        public IActionResult GeneratePassword()
        {
            var securePassword = _passwordService.GenerateSecurePassword();

            _logService.LogAction(4, " a généré un mot de passe");

            return Ok(securePassword);
        }

        [HttpGet("check-password")]
        public async Task<IActionResult> CheckPassword([FromQuery] string password)
        {
            if (string.IsNullOrEmpty(password))
                return BadRequest(new { Message = "Le mot de passe est requis." });

            var isWeak = await _passwordService.IsWeakPasswordAsync(password);

            _logService.LogAction(4, $" a checké le mot de passe {password}");

            return Ok(new
            {
                IsWeak = isWeak,
                Message = isWeak
                    ? "Ce mot de passe est trop courant. Veuillez utiliser un mot de passe plus fort"
                    : "Ce mot de passe ne figure pas dans la liste des mots de passe courants. Bon choix!"
            });
        }
    }
}

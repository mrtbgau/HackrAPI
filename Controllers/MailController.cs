using API.Services.Logs;
using API.Services.Mail;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MailController(IMailService mailService, ILogService logService) : Controller
    {
        private readonly ILogService _logService = logService;
        private readonly IMailService _mailService = mailService;
        [HttpGet("verify")]
        public async Task<IActionResult> VerifyEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest("Email is required.");

            var exists = await _mailService.VerifyEmailExistenceAsync(email);

            return exists ? Ok($"Le mail {email} existe.") : Ok($"Le mail {email} n'existe pas");
        }
    }
}

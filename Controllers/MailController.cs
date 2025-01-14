using API.Services.Logs;
using API.Services.Mail;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MailController(IMailService mailService, ILogService logService) : Controller
    {
        private readonly ILogService _logService = logService;
        private readonly IMailService _mailService = mailService;
        [HttpGet("verify-mail")]
        public async Task<IActionResult> VerifyEmail([FromQuery] string email)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest("Email is required.");

            var exists = await _mailService.VerifyEmailExistenceAsync(email);

            return exists ? Ok($"Le mail {email} existe.") : Ok($"Le mail {email} n'existe pas");
        }

        [HttpPost("spam_mail")]
        public IActionResult SpamMail([FromQuery] string email, [FromQuery] int count)
        {
            try
            {
                MailMessage mail = new()
                {
                    From = new MailAddress("bourguilleau.martin@gmail.com", "Martin Bourguilleau")
                };
                mail.To.Add(email);
                mail.Subject = "SPAM";
                mail.Body = "Tu es victime de SPAM";
                mail.IsBodyHtml = false;

                SmtpClient smtp = new("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("bourguilleau.martin@gmail.com", "fcto cgyw lybm hvnh"),
                    EnableSsl = true
                };

                for (int i = 0; i < count; i++)
                {
                    smtp.Send(mail);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }
    }
}

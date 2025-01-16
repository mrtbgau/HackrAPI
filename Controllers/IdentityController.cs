using API.Services.Identity;
using API.Services.Logs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentityController(IIdentityService identityService, ILogService logService) : Controller
    {
        private readonly IIdentityService _identityService = identityService;
        private readonly ILogService _logService = logService;

        [HttpGet("generate-identity")]
        public async Task<IActionResult> GenerateIdentity()
        {
            var identity = await _identityService.GenerateIdentityAsync();

            _logService.LogAction(4, " a généré une fausse identité");

            return Ok(identity);
        }

        [HttpGet("crawl")]
        public async Task<IActionResult> CrawlInformation([FromQuery] string firstName, [FromQuery] string lastName)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                return BadRequest(new { Message = "First name and last name are required." });
            }

            try
            {
                var result = await _identityService.SearchPersonAsync(firstName, lastName);

                _logService.LogAction(4, $" a crawlé {firstName} {lastName}");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }
    }
}

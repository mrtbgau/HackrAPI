using API.Services.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentityController(IIdentityService identityService) : Controller
    {
        private readonly IIdentityService _identityService = identityService;
        [HttpGet("generate-identity")]
        public async Task<IActionResult> GenerateIdentity()
        {
            var identity = await _identityService.GenerateIdentityAsync();
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
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }
    }
}

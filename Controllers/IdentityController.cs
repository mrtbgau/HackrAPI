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
    }
}

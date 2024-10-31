using API.Models;
using API.Services.JWT;
using API.Services.Logs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController(ILogService logService) : Controller
    {
        private readonly ILogService _logService = logService;

        [Route("logs")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Log>>> GetRecentLogs([FromQuery] int count)
        {
            return Ok(await _logService.GetRecentLogs(count));
        }

        [Route("users/{id}/logs")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Log>>> GetUserLogs(int id, [FromQuery] int count)
        {
            return Ok(await _logService.GetUserLogs(id, count));
        }
    }
}

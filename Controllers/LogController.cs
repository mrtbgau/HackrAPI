using API.Droits;
using API.Models;
using API.Services.Logs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [RequirePermission("ViewLogs")]
    public class LogController(ILogService logService) : Controller
    {
        private readonly ILogService _logService = logService;

        [Route("recent")]
        [HttpGet]
        public ActionResult<List<Log>> GetRecentLogs([FromQuery] int count)
        {
            return Ok(_logService.GetRecentLogs(count));
        }

        [Route("users/{id}/logs")]
        [HttpGet]
        public ActionResult<IEnumerable<Log>> GetUserLogs(int id, [FromQuery] int count)
        {
            return Ok( _logService.GetUserLogs(id, count));
        }
    }
}

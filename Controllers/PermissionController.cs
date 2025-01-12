using API.Droits;
using API.Models;
using API.Models.Droits;
using API.Services.Droits;
using API.Services.Logs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [RequirePermission("ManagePermission")]
    public class PermissionController(IPermissionService permissionService, ILogService logService) : Controller
    {
        private readonly IPermissionService _permissionService = permissionService;
        private readonly ILogService _logService = logService;

        [Route("users/{id}/permissions")]
        [HttpGet]
        public ActionResult<IEnumerable<Permission>> GetUserPermissions(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            int userId = int.Parse(userIdClaim.Value);

            var userName = User.FindFirst(ClaimTypes.Name)?.Value;

            _logService.LogAction(userId, $"Récupération des permissions pour l'utilisateur {userName}");
            return Ok(_permissionService.GetUserPermissions(id));
        }

        [Route("users/{id}/assign-role")]
        [HttpPost]
        public ActionResult AssignRoleToUser(int id, [FromQuery] string role)
        {
            return Ok(_permissionService.AssignRoleToUser(id, role));
        }

        [Route("users/{id}/remove-role")]
        [HttpPost]
        public ActionResult RemoveRoleFromUser(int id, [FromQuery] string role)
        {
            return Ok(_permissionService.RemoveRoleFromUser(id, role));
        }

        [Route("roles/{id}/add-permission")]
        [HttpPost]
        public ActionResult AddPermissionToRole(int id, [FromQuery] string role)
        {
            return Ok(_permissionService.AddPermissionToRole(id, role));
        }

        [Route("roles/{id}/remove-permission")]
        [HttpPost]
        public ActionResult RemovePermissionFromRole(int id, [FromQuery] string role)
        {
            return Ok(_permissionService.RemovePermissionFromRole(id, role));
        }
    }
}

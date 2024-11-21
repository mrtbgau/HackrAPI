using API.Droits;
using API.Models;
using API.Models.Droits;
using API.Services.Droits;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [RequirePermission("ManagePermission")]
    public class PermissionController(IPermissionService permissionService) : Controller
    {
        private readonly IPermissionService _permissionService = permissionService;
        
        [Route("users/{id}/permissions")]
        [HttpGet]
        public ActionResult<IEnumerable<Permission>> GetUserPermissions(int id)
        {
            return Ok(_permissionService.GetUserPermissions(id));
        }

        [Route("assign-role")]
        [HttpPost]
        public ActionResult AssignRoleToUser([FromQuery] int userId, [FromQuery] string role)
        {
            return Ok(_permissionService.AssignRoleToUser(userId, role));
        }
    }
}

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

using API.Models;
using API.Models.Droits;
using API.Services.Droits;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionController(IPermissionService permissionService) : Controller
    {
        private readonly IPermissionService _permissionService = permissionService;
        // GET: PermissionController
        [Route("users/{id}/permissions")]
        [HttpGet]
        public ActionResult<IEnumerable<Permission>> GetUserPermissions(int id)
        {
            return Ok(_permissionService.GetUserPermissions(id));
        }
    }
}

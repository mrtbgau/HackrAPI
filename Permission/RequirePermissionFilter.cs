using System.Security.Claims;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Permission
{
    public class RequirePermissionFilter(string permissionName, IPermissionService permissionService) : IAuthorizationFilter
    {
        private readonly string _permissionName = permissionName;
        private readonly IPermissionService _permissionService = permissionService;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int userIdInt))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (!_permissionService.UserHasPermission(userIdInt, _permissionName))
            {
                context.Result = new ForbidResult();
                return;
            }
        }
    }
}
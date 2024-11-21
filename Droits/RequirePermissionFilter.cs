using System.Security.Claims;
using API.Services.Droits;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Droits
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

            bool hasPermission = _permissionService.UserHasPermission(userIdInt, _permissionName);

            if (!hasPermission)
            {
                context.Result = new ForbidResult();
                return;
            }
        }
    }
}
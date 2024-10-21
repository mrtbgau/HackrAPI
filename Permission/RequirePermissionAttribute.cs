using Microsoft.AspNetCore.Mvc;

namespace API.Permission
{
    public class RequirePermissionAttribute : TypeFilterAttribute
    {
        public RequirePermissionAttribute(string permissionName) 
            : base(typeof(RequirePermissionFilter))
        {
            Arguments = [permissionName];
        }
    }
}
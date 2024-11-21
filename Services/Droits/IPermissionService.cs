using API.Models.Droits;

namespace API.Services.Droits
{
    public interface IPermissionService
    {
        bool UserHasPermission(int userId, string permissionName);
        IEnumerable<Permission?> GetUserPermissions(int userId);
        bool AssignRoleToUser(int userId, string role);
        bool RemoveRoleFromUser(int userId, string role);
        bool AddPermissionToRole(string role, string permission);
        bool RemovePermissionFromRole(string role, string permission);
    }
}
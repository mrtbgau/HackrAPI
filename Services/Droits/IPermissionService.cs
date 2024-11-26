using API.Models.Droits;

namespace API.Services.Droits
{
    public interface IPermissionService
    {
        bool UserHasPermission(int userId, string permissionName);
        IEnumerable<Permission?> GetUserPermissions(int userId);
        bool AssignRoleToUser(int userId, string role);
        bool RemoveRoleFromUser(int userId, string role);
        bool AddPermissionToRole(int roleId, string permission);
        bool RemovePermissionFromRole(int roleId, string permission);
    }
}
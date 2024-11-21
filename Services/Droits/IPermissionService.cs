using API.Models.Droits;

namespace API.Services.Droits
{
    public interface IPermissionService
    {
        bool UserHasPermission(int userId, string permissionName);
        IEnumerable<Permission?> GetUserPermissions(int userId);
        bool AssignRoleToUser(int userId, int roleId);
        bool RemoveRoleFromUser(int userId, int roleId);
        bool AddPermissionToRole(int roleId, int permissionId);
        bool RemovePermissionFromRole(int roleId, int permissionId);
    }
}
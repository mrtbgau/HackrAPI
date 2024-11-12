using API.Models.Droits;

namespace API.Services.Droits
{
    public interface IPermissionService
    {
        bool UserHasPermission(int userId, string permissionName);
        IEnumerable<Permission> GetUserPermissions(int userId);
        Task<bool> AssignRoleToUser(int userId, int roleId);
        Task<bool> AddPermissionToRole(int roleId, int permissionId);
        Task<bool> RemovePermissionFromRole(int roleId, int permissionId);
    }
}
using API.Models.Droits;

namespace API.Services.Droits
{
    public interface IPermissionService
    {
        Task<bool> UserHasPermission(int userId, string permissionName);
        Task<IEnumerable<Permission?>> GetUserPermissions(int userId);
        Task<bool> AssignRoleToUser(int userId, int roleId);
        Task<bool> RemoveRoleFromUser(int userId, int roleId);
        Task<bool> AddPermissionToRole(int roleId, int permissionId);
        Task<bool> RemovePermissionFromRole(int roleId, int permissionId);
    }
}
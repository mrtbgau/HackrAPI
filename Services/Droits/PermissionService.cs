using API.Models;
using API.Models.Droits;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Droits
{
    public class PermissionService(DbAPIContext dbAPIContext) : IPermissionService{
        private readonly DbAPIContext dbAPIContext = dbAPIContext;

        public bool UserHasPermission(int userId, string permissionName)
        {
            return GetUserRoleByUserId(userId)
                .SelectMany(r => r!.RolePermissions!.Select(rp => rp.Permission))
                .Any(p => p!.Name == permissionName);
        }

        public IEnumerable<Permission?> GetUserPermissions(int userId)
        {
            return GetUserRoleByUserId(userId)
                .SelectMany(r => r!.RolePermissions!.Select(rp => rp.Permission))
                .Distinct();
        }

        public async Task<bool> AssignRoleToUser(int userId, int roleId)
        {
            var user = await dbAPIContext.Users.FirstOrDefaultAsync(u => u.UserID == userId);
            var role = await dbAPIContext.Roles.FirstOrDefaultAsync(r => r.RoleId == roleId);

            if (user == null || role == null)
                return false;

            if (user.Role != null && user.Role.RoleId == roleId)
                return false;

            user.Role = role;
            await dbAPIContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveRoleFromUser(int userId, int roleId)
        {
            var user = await dbAPIContext.Users.FirstOrDefaultAsync(u => u.UserID == userId);

            if (user == null || user.Role == null || user.Role.RoleId != roleId)
                return false;

            user.Role = null;
            await dbAPIContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddPermissionToRole(int roleId, int permissionId)
        {
            if (await dbAPIContext.RolePermissions.AnyAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId))
                return false;

            dbAPIContext.RolePermissions.Add(new RolePermission { RoleId = roleId, PermissionId = permissionId });
            await dbAPIContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemovePermissionFromRole(int roleId, int permissionId)
        {
            var rolePermission = await dbAPIContext.RolePermissions
                .FirstOrDefaultAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);
            
            if (rolePermission == null)
                return false;

            dbAPIContext.RolePermissions.Remove(rolePermission);
            await dbAPIContext.SaveChangesAsync();
            return true;
        }

        private IQueryable<Role?> GetUserRoleByUserId(int userId)
        {
            return dbAPIContext.Users
                .Where(u => u.UserID == userId)
                .Select(u => u.Role);
        }
    }
}
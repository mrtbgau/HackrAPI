using API.Models.Droits;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Droits
{
    public class PermissionService(DbAPIContext dbAPIContext) : IPermissionService{
        private readonly DbAPIContext dbAPIContext = dbAPIContext;

        public bool UserHasPermission(int userId, string permissionName)
        {
            return dbAPIContext.Users
                .Where(u => u.UserID == userId)
                .SelectMany(u => u.UserRoles)
                .SelectMany(ur => ur.Role.RolePermissions)
                .Any(rp => rp.Permission.Name == permissionName);
        }

        public IEnumerable<Permission> GetUserPermissions(int userId)
        {
            return dbAPIContext.Users
                .Where(u => u.UserID == userId)
                .SelectMany(u => u.UserRoles)
                .SelectMany(ur => ur.Role.RolePermissions)
                .Select(rp => rp.Permission)
                .Distinct();
        }

        public async Task<bool> AssignRoleToUser(int userId, int roleId)
        {
            if (await dbAPIContext.UserRoles.AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId))
                return false;

            dbAPIContext.UserRoles.Add(new UserRole { UserId = userId, RoleId = roleId });
            await dbAPIContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveRoleFromUser(int userId, int roleId)
        {
            var userRole = await dbAPIContext.UserRoles
                .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
            
            if (userRole == null)
                return false;

            dbAPIContext.UserRoles.Remove(userRole);
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
    }
}
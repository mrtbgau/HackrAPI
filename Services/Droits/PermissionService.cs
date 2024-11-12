using API.Models;
using API.Models.Droits;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Droits
{
    public class PermissionService(DbAPIContext dbAPIContext) : IPermissionService{
        private readonly DbAPIContext dbAPIContext = dbAPIContext;

        public bool UserHasPermission(int userId, string? permissionName)
        {
            return GetUserRoleByUserId(userId)
                .SelectMany(r => r.RolePermissions)
                .Any(rp => rp.Permission.Name == permissionName);
        }

        public IEnumerable<Permission> GetUserPermissions(int userId)
        {
            return GetUserRoleByUserId(userId)
                .SelectMany(r => r.RolePermissions)
                .Select(r => r.Permission);
        }

        public async Task<bool> AssignRoleToUser(int userId, int roleId)
        {
            User currentUser = dbAPIContext.Users.Where(u => u.UserID == userId).FirstOrDefault();

            if (currentUser.RoleId != roleId)
            {
                await dbAPIContext.SaveChangesAsync();
                return true;
            }

            return false;
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

        private IQueryable<Role> GetUserRoleByUserId(int userId)
        {
            return dbAPIContext.Users
                .Where(u => u.UserID == userId)
                .Select(u => u.Role);
        }
    }
}
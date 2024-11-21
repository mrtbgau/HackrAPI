using API.Models;
using API.Models.Droits;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Droits
{
    public class PermissionService(DbAPIContext dbAPIContext) : IPermissionService{
        private readonly DbAPIContext dbAPIContext = dbAPIContext;

        public async Task<bool> UserHasPermission(int userId, string permissionName)
        {
            return await dbAPIContext.Users
                .Where(u => u.UserID == userId)
                .SelectMany(u => u.Role!.RolePermissions!)
                .Select(rp => rp.Permission)
                .AnyAsync(p => p!.Name == permissionName);
        }

        public async Task<IEnumerable<Permission?>> GetUserPermissions(int userId)
        {
            return await dbAPIContext.Users
                .Where(u => u.UserID == userId)
                .SelectMany(u => u.Role!.RolePermissions!)
                .Select(rp => rp.Permission!)
                .Distinct()
                .ToListAsync();
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
            user.RoleId = roleId;

            try
            {
                await dbAPIContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public async Task<bool> RemoveRoleFromUser(int userId, int roleId)
        {
            var user = await dbAPIContext.Users.FindAsync(userId);

            if (user == null || user.Role?.RoleId != roleId)
                return false;

            user.Role = null;

            try
            {
                await dbAPIContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public async Task<bool> AddPermissionToRole(int roleId, int permissionId)
        {
            var role = await dbAPIContext.Roles.FindAsync(roleId);
            var permission = await dbAPIContext.Permissions.FindAsync(permissionId);

            if (role == null || permission == null)
                return false;

            var existingRolePermission = await dbAPIContext.RolePermissions
                .FirstOrDefaultAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);

            if (existingRolePermission != null)
                return false;

            var rolePermission = new RolePermission
            {
                RoleId = roleId,
                PermissionId = permissionId
            };

            dbAPIContext.RolePermissions.Add(rolePermission);

            try
            {
                await dbAPIContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public async Task<bool> RemovePermissionFromRole(int roleId, int permissionId)
        {
            var rolePermission = await dbAPIContext.RolePermissions
                .FirstOrDefaultAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);

            if (rolePermission == null)
                return false;

            dbAPIContext.RolePermissions.Remove(rolePermission);

            try
            {
                await dbAPIContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }
    }
}
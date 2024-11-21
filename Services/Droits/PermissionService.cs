using API.Models;
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
                .SelectMany(u => u.Role!.RolePermissions!)
                .Select(rp => rp.Permission)
                .Any(p => p!.Name == permissionName);
        }

        public IEnumerable<Permission?> GetUserPermissions(int userId)
        {
            return dbAPIContext.Users
                .Where(u => u.UserID == userId)
                .SelectMany(u => u.Role!.RolePermissions!)
                .Select(rp => rp.Permission!)
                .Distinct()
                .ToList();
        }

        public bool AssignRoleToUser(int userId, int roleId)
        {
            var user = dbAPIContext.Users.FirstOrDefault(u => u.UserID == userId);
            var role = dbAPIContext.Roles.FirstOrDefault(r => r.RoleId == roleId);

            if (user == null || role == null)
                return false;

            if (user.Role != null && user.Role.RoleId == roleId)
                return false;

            user.Role = role;
            user.RoleId = roleId;

            try
            {
                dbAPIContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public bool RemoveRoleFromUser(int userId, int roleId)
        {
            var user = dbAPIContext.Users.Find(userId);

            if (user == null || user.Role?.RoleId != roleId)
                return false;

            user.Role = null;

            try
            {
                dbAPIContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public bool AddPermissionToRole(int roleId, int permissionId)
        {
            var role = dbAPIContext.Roles.Find(roleId);
            var permission = dbAPIContext.Permissions.Find(permissionId);

            if (role == null || permission == null)
                return false;

            var existingRolePermission = dbAPIContext.RolePermissions
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
                dbAPIContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public bool RemovePermissionFromRole(int roleId, int permissionId)
        {
            var rolePermission = dbAPIContext.RolePermissions
                .FirstOrDefault(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);

            if (rolePermission == null)
                return false;

            dbAPIContext.RolePermissions.Remove(rolePermission);

            try
            {
                dbAPIContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }
    }
}
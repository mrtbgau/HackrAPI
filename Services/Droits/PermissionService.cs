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

        public bool AssignRoleToUser(int userId, string role)
        {
            var user = dbAPIContext.Users.FirstOrDefault(u => u.UserID == userId);
            var roleName = dbAPIContext.Roles.FirstOrDefault(r => r.Name == role);

            if (user == null || role == null)
                return false;

            if (user.Role != null && user.Role.Name == roleName?.ToString())
                return false;

            user.Role = roleName;
            user.RoleId = roleName!.RoleId;

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

        public bool RemoveRoleFromUser(int userId, string role)
        {
            var user = dbAPIContext.Users.Find(userId);

            if (user == null || user.Role?.Name != role)
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

        public bool AddPermissionToRole(string role, string permission)
        {
            var roleName = dbAPIContext.Roles.FirstOrDefault(r => r.Name == role);
            var permissionName = dbAPIContext.Permissions.First(r => r.Name == permission);

            if (role == null || permission == null)
                return false;

            var existingRolePermission = dbAPIContext.RolePermissions
                .FirstOrDefaultAsync(rp => rp.RoleId == roleName!.RoleId && rp.PermissionId == permissionName.PermissionId);

            if (existingRolePermission != null)
                return false;

            var rolePermission = new RolePermission
            {
                RoleId = roleName!.RoleId,
                PermissionId = permissionName.PermissionId
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

        public bool RemovePermissionFromRole(string role, string permission)
        {
            var roleName = dbAPIContext.Roles.FirstOrDefault(r => r.Name == role);
            var permissionName = dbAPIContext.Permissions.First(r => r.Name == permission);

            var rolePermission = dbAPIContext.RolePermissions
                .FirstOrDefault(rp => rp.RoleId == roleName.RoleId && rp.PermissionId == permissionName.PermissionId);

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
using Microsoft.AspNetCore.Identity;
using TodoList.Data;

namespace TodoList.Provider
{
    public class UserRoleTable
    {
        private readonly ApplicationDbContext context;

        public UserRoleTable(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<string>> FindByUserIdAsync(int userId)
        {
            var roles = await context.UserRoles.Where(x => x.UserId == userId)
                .Join(context.Roles,
                userRole => userRole.RoleId,
                role => role.Id,
                (userRole, role) =>
                    new
                    {
                        role = role.Name
                    }
                )
                .Select(x => x.role)
                .ToListAsync();
            return roles;
        }

        public async Task<IdentityResult> DeleteAsync(int userId)
        {
            var userRole = await context.UserRoles.FindAsync(userId);
            if (userRole != null)
            {
                context.UserRoles.Remove(userRole);
                await context.SaveChangesAsync();
            }
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> AddUserRoleAsync(ApplicationUser user, ApplicationRole role)
        {
            var userRole = new IdentityUserRole<int>
            {
                UserId = user.Id,
                RoleId = role.Id
            };
            context.UserRoles.Add(userRole);
            await context.SaveChangesAsync();
            return IdentityResult.Success;
        }
    }
}

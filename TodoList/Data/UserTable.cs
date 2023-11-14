using Microsoft.AspNetCore.Identity;
using System.Globalization;
using TodoList.Models.Entities;

namespace TodoList.Data
{
    public class UserTable
    {
        private readonly ApplicationDbContext _context;

        public UserTable(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            await SetNormalizedUserNameAsync(user, user.UserName.ToUpper());
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationUser user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public async void Dispose()
        {
            await _context.DisposeAsync();
        }

        public async Task<ApplicationUser?> FindByIdAsync(string userId)
        {
            var user = await _context.Users.FindAsync(int.Parse(userId));
            return user;
        }

        public async Task<ApplicationUser?> FindByNameAsync(string normalizedUserName)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == normalizedUserName);
            return user;
        }

        public async Task<string> GetUserIdAsync(ApplicationUser user)
        {
            return await Task.FromResult(Convert.ToString(user.Id));
        }

        public async Task<string?> GetUserNameAsync(ApplicationUser user)
        {
            var userName = user.UserName;
            return await Task.FromResult(userName);
        }

        public async Task<object> SetUserNameAsync(ApplicationUser user, string userName)
        {
            return await Task.FromResult<object>(null);
        }
        public async Task<IdentityResult> UpdateAsync(ApplicationUser user)
        {
            _context.Update(user);
            var affectedRows = await _context.SaveChangesAsync();
            return affectedRows > 0
                ? IdentityResult.Success
                : IdentityResult.Failed(new IdentityError() 
                {
                    Description = $"Could not update user {user.UserName}." 
                });
        }

        public async Task AddToRoleAsync(ApplicationUser user, string roleName)
        {
            var roleEntity = await _context.Roles.FirstOrDefaultAsync(r => r.NormalizedName == roleName);
            if (roleEntity == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture,
                    "Role Not Found", roleName));
            }
            var ur = new ApplicationUserRoles { UserId = user.Id, RoleId = roleEntity.Id };
            _context.UserRoles.Add(ur);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            var roles = await _context.UserRoles.Where(x => x.UserId == user.Id)
                .Join(_context.Roles,
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
            return (IList<string>)roles;
        }

        public async Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.NormalizedName == roleName);
            var users = await _context.Users.Join(_context.Roles,
                user => user.Id,
                role => role.Id,
                (user,role) =>
                new
                {
                    user
                })
                .ToListAsync();
            return (IList<ApplicationUser>)users;
        }

        public async Task<bool> IsInRoleAsync(ApplicationUser user, string roleName)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.NormalizedName == roleName);
            if (role != null)
            {
                var userId = user.Id;
                var roleId = role.Id;
                return await _context.UserRoles.AnyAsync(ur => ur.RoleId.Equals(roleId) && ur.UserId.Equals(userId));
            }
            return false;
        }

        public async Task RemoveFromRoleAsync(ApplicationUser user, string roleName)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.NormalizedName == roleName);
            var userRole = await _context.UserRoles.SingleOrDefaultAsync(r =>
                r.RoleId == role.Id && r.UserId == user.Id);
            _context.UserRoles.Remove(userRole);
            await _context.SaveChangesAsync();
        }

        public async Task SetNormalizedUserNameAsync(ApplicationUser user, string? normalizedName)
        {
            var userDB = await _context.Users.FirstOrDefaultAsync(u => u.UserName == user.UserName);
            if (userDB != null)
            {
                userDB.NormalizedUserName = normalizedName;
                _context.Update(userDB);
            }
            await _context.SaveChangesAsync();
        }
    }
}

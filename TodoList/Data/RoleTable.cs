using Microsoft.AspNetCore.Identity;
using TodoList.Data;

namespace TodoList.Provider
{
    public class RoleTable
    {
        private readonly ApplicationDbContext _context;

        public RoleTable(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IdentityResult> CreateAsync(ApplicationRole role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationRole role)
        {
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public void Dispose() 
        {
            this.Dispose();
        }

        public async Task<ApplicationRole?> FindByIdAsync(string roleId)
        {
            var role = await _context.Roles.FindAsync(roleId);
            return role;
        }

        public async Task<ApplicationRole?> FindByNameAsync(string normalizedRoleName)
        {
            var role = await _context.Roles.FindAsync(normalizedRoleName);
            return role;
        }

        public async Task<string> GetRoleIdAsync(ApplicationRole role)
        {
            return await Task.FromResult(Convert.ToString(role.Id));
        }

        public async Task<string?> GetRoleNameAsync(ApplicationRole role)
        {
            return await Task.FromResult(role.Name);
        }

        public async Task<object> SetRoleNameAsync(ApplicationRole role, string roleName)
        {
            role.Name = roleName;
            return await Task.FromResult<object>(null);
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationRole role)
        {
            _context.Update(role);
            var affectedRows = await _context.SaveChangesAsync();
            return affectedRows > 0
                ? IdentityResult.Success
                : IdentityResult.Failed(new IdentityError() 
                {
                    Description = $"Could not update role {role.Name}." 
                });
        }
    }
}

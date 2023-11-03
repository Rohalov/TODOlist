using Microsoft.AspNetCore.Identity;


namespace TodoList.Provider
{
    public class RoleStore : IRoleStore<ApplicationRole>
    {
        private readonly RoleTable roleTable;

        public RoleStore(RoleTable roleTable)
        {
            this.roleTable = roleTable;
        }

        public async Task<IdentityResult> CreateAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            return await roleTable.CreateAsync(role);
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            return await roleTable.DeleteAsync(role);
        }

        public void Dispose()
        {
            roleTable.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<ApplicationRole?> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await roleTable.FindByIdAsync(roleId);
        }

        public async Task<ApplicationRole?> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await roleTable.FindByNameAsync(normalizedRoleName);
        }

        public async Task<string?> GetNormalizedRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            return await Task.FromResult(role.Name);
        }

        public async Task<string> GetRoleIdAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await roleTable.GetRoleIdAsync(role);
        }

        public async Task<string?> GetRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await roleTable.GetRoleNameAsync(role);
        }

        public Task SetNormalizedRoleNameAsync(ApplicationRole role, string? normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult<object>(null);
        }

        public async Task<object> SetRoleNameAsync(ApplicationRole role, string? roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            return await roleTable.SetRoleNameAsync(role, roleName);
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            return await roleTable.UpdateAsync(role);
        }

        Task IRoleStore<ApplicationRole>.SetRoleNameAsync(ApplicationRole role, string? roleName, CancellationToken cancellationToken)
        {
            return this.SetRoleNameAsync(role, roleName, cancellationToken);
        }
    }
}

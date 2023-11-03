using Microsoft.AspNetCore.Mvc;

namespace TodoList.Provider
{
    public interface IAuthenticationService
    {
        Task<ApplicationUser> Login(ApplicationUser user);
        Task<ApplicationUser> Register(ApplicationUser user);
    }
}

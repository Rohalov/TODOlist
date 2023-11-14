using TodoList.Models.Entities;

namespace TodoList.Services
{
    public interface IAuthenticationService
    {
        Task<string> Login(ApplicationUser user);
        Task<ApplicationUser> Register(ApplicationUser user);
    }
}

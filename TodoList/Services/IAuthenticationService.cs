using TodoList.Models.Entities;

namespace TodoList.Services
{
    public interface IAuthenticationService
    {
        Task<ApplicationUser> Login(ApplicationUser user);
        Task<ApplicationUser> Register(ApplicationUser user);
    }
}

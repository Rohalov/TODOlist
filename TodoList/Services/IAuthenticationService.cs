using TodoList.Models.DTO;
using TodoList.Models.Entities;

namespace TodoList.Services
{
    public interface IAuthenticationService
    {
        Task<string> Login(UserDTO user);
        Task<ApplicationUser> Register(ApplicationUser user);
    }
}

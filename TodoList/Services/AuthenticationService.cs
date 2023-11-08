using Microsoft.AspNetCore.Identity;
using TodoList.Models.Entities;

namespace TodoList.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthenticationService(UserManager<ApplicationUser> userManager) 
        {
            _userManager = userManager;
        }

        public async Task<ApplicationUser> Register(ApplicationUser user)
        {
            var userInDB = await _userManager.FindByNameAsync(user.UserName);
            if (userInDB != null) 
            {
                return null;
            }
            await _userManager.CreateAsync(user);
            await _userManager.AddToRoleAsync(user, "User");
            return user;
        }

        public async Task<ApplicationUser> Login(ApplicationUser user)
        {
            var userInDB = await _userManager.FindByNameAsync(user.UserName);
            if(userInDB == null)
            {
                return null;
            }
            return userInDB;
        }
    }
}

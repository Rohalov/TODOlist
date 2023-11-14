using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoList.Models.Entities;

namespace TodoList.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticationService(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<ApplicationUser> Register(ApplicationUser newUser)
        {
            var user = await _userManager.FindByNameAsync(newUser.UserName);
            if (user != null)
            {
                return null;
            }
            var isCreated = await _userManager.CreateAsync(newUser);
            var createdUser = new ApplicationUser();
            if (isCreated.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, "User");
                createdUser = await _userManager.FindByNameAsync(newUser.UserName);
            }
            return createdUser;
        }

        public async Task<string> Login(ApplicationUser user)
        {
            var userInDB = await _userManager.FindByNameAsync(user.UserName);
            if (userInDB == null)
            {
                return null;
            }
            var jwt = await GenerateJwtToken(userInDB);
            return jwt;
        }

        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            List<Claim> claims = await GetAllValidClaims(user);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                            _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: creds,
                expires: DateTime.Now.AddDays(1)
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }


        private async Task<List<Claim>> GetAllValidClaims(ApplicationUser user)
        {
            var _option = new IdentityOptions();

            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                var role = await _roleManager.FindByNameAsync(userRole);
                if (role != null)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.Name));
                }
            }

            return claims;
        }
    }
}

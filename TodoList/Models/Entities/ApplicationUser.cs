using Microsoft.AspNetCore.Identity;

namespace TodoList.Provider
{
    public class ApplicationUser : IdentityUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
    }
}

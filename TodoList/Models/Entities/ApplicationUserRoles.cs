using Microsoft.AspNetCore.Identity;

namespace TodoList.Models.Entities
{
    public class ApplicationUserRoles : IdentityUserRole<int>
    {
        public int Id { get; set; }
    }
}

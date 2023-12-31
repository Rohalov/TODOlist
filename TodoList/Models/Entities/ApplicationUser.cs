﻿using Microsoft.AspNetCore.Identity;

namespace TodoList.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
    }
}

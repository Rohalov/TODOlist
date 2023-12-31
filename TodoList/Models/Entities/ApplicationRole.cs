﻿using Microsoft.AspNetCore.Identity;

namespace TodoList.Models.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

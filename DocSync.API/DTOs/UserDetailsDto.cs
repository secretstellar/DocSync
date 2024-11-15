﻿using DocSync.API.Models;

namespace DocSync.API.DTOs
{
    public class UserDetailsDto : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

    }
}

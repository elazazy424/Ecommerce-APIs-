﻿using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Api.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email{ get; set; }
        [Required]
        public string Password { get; set; }
    }
}

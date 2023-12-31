﻿using System.ComponentModel.DataAnnotations;


namespace Client.Models.DTO.IdentityDTO
{
    public class LoginDTO
    {
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}

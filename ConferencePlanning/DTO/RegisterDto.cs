﻿using System.ComponentModel.DataAnnotations;

namespace ConferencePlanning.DTO;

public class RegisterDto
{
    [Required]
    public string DisplayName { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{4,8}$",
        ErrorMessage = "Password must contain the uppercase and lowercase letters, numbers and symbols")]
    public string Password { get; set; }
    
    [Required]
    public string UserName { get; set; }
}

﻿using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Users;

namespace WebApiForHikka.WebApi.Dto.Users;
public class UserRegistrationDto
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [RegularExpression(UserStringConstants.SimplePasswordRegExpression, ErrorMessage = UserStringConstants.SimplePasswordErrorMessage)]
    public required string Password { get; set; }

    [Required]
    public required string Role { get; set; }
}

﻿using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.Users;
using WebApiForHikka.Constants.Controllers;
using WebApiForHikka.Dtos.MyOwnValidationAttribute;

namespace WebApiForHikka.Dtos.Dto.Users;
public class UpdateUserDto
{
    [Required(ErrorMessage = ControllerStringConstants.IdIsRequiredErrorMessage)]
    public required Guid Id { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = ControllerStringConstants.EmailIsntFormatedCorrectlyErrorMessage)]
    public required string Email { get; set; }

    [Required]
    [RoleValidation(ErrorMessage = UserStringConstants.RoleDoesntExist)]
    public required string Role { get; set; }
}

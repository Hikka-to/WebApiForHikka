﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Application.Users;
using WebApiForHikka.Constants.Controllers;
using WebApiForHikka.Constants.Models.Users;
using WebApiForHikka.Domain;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.Users;
using WebApiForHikka.Dtos.ResponseDto;
using WebApiForHikka.SharedFunction.JwtTokenFactories;
using WebApiForHikka.WebApi.Shared;
using WebApiForHikka.WebApi.Shared.ErrorEndPoints;

namespace WebApiForHikka.WebApi.Controllers;


[Authorize(Policy = ControllerStringConstants.CanAccessOnlyAdmin)]
public class UsersController
    (
        IUserService userService,
        IJwtTokenFactory jwtTokenFactory,
        IConfiguration configuration,
        UserManager<User> userManager,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor
    )
    : MyBaseController(mapper, httpContextAccessor),
    ICrudController<UpdateUserDto, UserRegistrationDto>
{
    private readonly IUserService _userService = userService;
    private readonly IConfiguration _configuration = configuration;
    private readonly IJwtTokenFactory _jwtTokenFactory = jwtTokenFactory;

    [AllowAnonymous]
    [HttpPost("Registrate")]
    public async Task<IActionResult> Create([FromBody] UserRegistrationDto model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(GetAllErrorsDuringValidation());
        }

        var user = new User
        {
            UserName = model.UserName,
            Email = model.Email,
            PasswordHash = model.Password,
            Role = model.Role,
        };

        var id = await _userService.RegisterUserAsync(user, cancellationToken);

        if (id == null)
        {
            return BadRequest(UserStringConstants.MessageUserIsntRegistrated);
        }


        return Ok(new RegistratedResponseUserDto() { Message = UserStringConstants.MessageUserRegistrated, Id = (Guid)id });
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(GetAllErrorsDuringValidation());
        }

        var user = await _userService.AuthenticateUserAsync(model.Email, model.Password, cancellationToken);
        if (user == null) return Unauthorized();


        string tokenString = _jwtTokenFactory.GetJwtToken(user, _configuration)!;

        return Ok(new LoginResponseUserDto() { Token = tokenString });
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll([FromQuery] FilterPaginationDto paginationDto, CancellationToken cancellationToken)
    {
        string[] rolesToAccessTheEndpoint = [UserStringConstants.AdminRole];
        ErrorEndPoint errorEndPoint = ValidateRequest(new ThingsToValidateBase() { RolesToAccessTheEndPoint = rolesToAccessTheEndpoint, });
        if (errorEndPoint.IsError)
        {
            return errorEndPoint.GetError();
        }

        var paginationCollection = await _userService.GetAllAsync(paginationDto, cancellationToken);

        var users = _mapper.Map<List<GetUserDto>>(paginationCollection.Models);
        return Ok(
            new ReturnUserPageDto()
            {
                HowManyPages = (int)Math.Ceiling((double)paginationCollection.Total / paginationDto.PageSize),
                Models = users,
            }

        );
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<GetUserDto>(await _userService.GetAsync(id, cancellationToken));
        if (user is null)
            return NotFound();

        return Ok(user);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateUserDto dto, CancellationToken cancellationToken)
    {
        string[] rolesToAccessTheEndpoint = [UserStringConstants.AdminRole];
        ErrorEndPoint errorEndPoint = ValidateRequest(
            new ThingsToValidateBase()
            {
                RolesToAccessTheEndPoint = rolesToAccessTheEndpoint,
            }
            );
        if (errorEndPoint.IsError)
        {
            return errorEndPoint.GetError();
        }

        var user = _mapper.Map<User>(dto);

        var userWithPassword = await _userService.GetAsync(dto.Id, cancellationToken);
        if (userWithPassword == null)
        {
            return BadRequest($"user with {dto.Id} doesn't exist");
        }
        await _userService.UpdateAsync(user, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await _userService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Application.Users;
using WebApiForHikka.Constants.Controllers;
using WebApiForHikka.Constants.Models.Users;
using WebApiForHikka.Domain;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.SharedDtos;
using WebApiForHikka.Dtos.Dto.Users;
using WebApiForHikka.Dtos.ResponseDto;
using WebApiForHikka.SharedFunction.JwtTokenFactories;
using WebApiForHikka.WebApi.Shared;
using WebApiForHikka.WebApi.Shared.ErrorEndPoints;

namespace WebApiForHikka.WebApi.Controllers;


[Authorize(ControllerStringConstants.CanAccessOnlyAdmin)]
public class UserController
    (
        IUserService userService,
        IJwtTokenFactory jwtTokenFactory,
        IConfiguration configuration,
        RoleManager<IdentityRole<Guid>> roleManager,
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

        var role = await roleManager.FindByNameAsync(model.Role);


        var user = new User
        {
            UserName = model.UserName,
            Email = model.Email,
            PasswordHash = model.Password,
            Roles = [role!],
        };

        var id = await _userService.RegisterUserAsync(user, cancellationToken);

        if (id == null)
        {
            return BadRequest(UserStringConstants.MessageUserIsntRegistrated);
        }

        var tokenString = await _jwtTokenFactory.GetJwtTokenAsync(user, _configuration);

        return Ok(new RegistratedResponseUserDto() { Message = UserStringConstants.MessageUserRegistrated, JwtToken = tokenString,  Id = (Guid)id }) ;
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


        var tokenString = await _jwtTokenFactory.GetJwtTokenAsync(user, _configuration);

        return Ok(new LoginResponseUserDto() { Token = tokenString! });
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll([FromQuery] FilterPaginationDto paginationDto, CancellationToken cancellationToken)
    {
        ErrorEndPoint errorEndPoint = ValidateRequest(new ThingsToValidateBase() {  });
        if (errorEndPoint.IsError)
        {
            return errorEndPoint.GetError();
        }

        FilterPagination filterPagination = _mapper.Map<FilterPagination>(paginationDto);

        var paginationCollection = await _userService.GetAllAsync(filterPagination, cancellationToken);

        var users = _mapper.Map<List<GetUserDto>>(paginationCollection.Models);
        return Ok(
            new ReturnUserPageDto()
            {
                HowManyPages = (int)Math.Ceiling((double)paginationCollection.Total / filterPagination.PageSize),
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
        ErrorEndPoint errorEndPoint = ValidateRequest(
            new ThingsToValidateBase()
            {
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
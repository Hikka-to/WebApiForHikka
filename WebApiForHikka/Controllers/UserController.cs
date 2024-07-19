using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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

namespace WebApiForHikka.WebApi.Controllers;

[Authorize(ControllerStringConstants.CanAccessOnlyAdmin)]
public class UserController(
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
    private readonly IConfiguration _configuration = configuration;
    private readonly IJwtTokenFactory _jwtTokenFactory = jwtTokenFactory;
    private readonly IUserService _userService = userService;

    [AllowAnonymous]
    [HttpPost("Registration")]
    [SwaggerResponse(StatusCodes.Status200OK, "User registered", typeof(RegistratedResponseUserDto))]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Model Validation Error",
        typeof(IDictionary<string, IEnumerable<string>>))]
    public async Task<IActionResult> Create([FromBody] UserRegistrationDto model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return UnprocessableEntity(GetAllErrorsDuringValidation());

        var role = await roleManager.FindByNameAsync(model.Role);

        var user = new User
        {
            UserName = model.UserName,
            Email = model.Email,
            PasswordHash = model.Password,
            Roles = [role!]
        };

        var id = await _userService.RegisterUserAsync(user, cancellationToken);

        if (id == null) return NotFound(UserStringConstants.MessageUserIsntRegistrated);

        var tokenString = await _jwtTokenFactory.GetJwtTokenAsync(user, _configuration);

        return Ok(new RegistratedResponseUserDto
            {
                Message = UserStringConstants.MessageUserRegistrated,
                JwtToken = tokenString,
                Id = (Guid)id
            }
        );
    }

    [HttpGet("GetAll")]
    [SwaggerResponse(StatusCodes.Status200OK, "Return all users", typeof(ReturnUserPageDto))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Model Validation Error",
        typeof(IDictionary<string, IEnumerable<string>>))]
    public async Task<IActionResult> GetAll([FromQuery] FilterPaginationDto paginationDto,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var filterPagination = _mapper.Map<FilterPagination>(paginationDto);

        var paginationCollection = await _userService.GetAllAsync(filterPagination, cancellationToken);

        var users = _mapper.Map<List<GetUserDto>>(paginationCollection.Models);
        return Ok(
            new ReturnUserPageDto
            {
                HowManyPages = (int)Math.Ceiling((double)paginationCollection.Total / filterPagination.PageSize),
                Models = users
            }
        );
    }

    [HttpGet("{id:Guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Return user by id", typeof(GetUserDto))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Model Validation Error",
        typeof(IDictionary<string, IEnumerable<string>>))]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var user = _mapper.Map<GetUserDto>(await _userService.GetAsync(id, cancellationToken));
        if (user is null)
            return NotFound();

        return Ok(user);
    }

    [HttpPut]
    [SwaggerResponse(StatusCodes.Status204NoContent, "User updated")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User not found", typeof(string))]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Model Validation Error",
        typeof(IDictionary<string, IEnumerable<string>>))]
    public async Task<IActionResult> Put([FromBody] UpdateUserDto dto, CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(
            new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var user = _mapper.Map<User>(dto);

        var userWithPassword = await _userService.GetAsync(dto.Id, cancellationToken);
        if (userWithPassword == null) return NotFound($"user with {dto.Id} doesn't exist");
        await _userService.UpdateAsync(user, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:Guid}")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "User deleted")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Model Validation Error",
        typeof(IDictionary<string, IEnumerable<string>>))]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        await _userService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    [SwaggerResponse(StatusCodes.Status200OK, "User logged in", typeof(LoginResponseUserDto))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Model Validation Error",
        typeof(IDictionary<string, IEnumerable<string>>))]
    public async Task<IActionResult> Login([FromBody] UserLoginDto model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return BadRequest(GetAllErrorsDuringValidation());

        var user = await _userService.AuthenticateUserAsync(model.Email, model.Password, cancellationToken);
        if (user == null) return Unauthorized();


        var tokenString = await _jwtTokenFactory.GetJwtTokenAsync(user, _configuration);

        return Ok(new LoginResponseUserDto { Token = tokenString! });
    }
}
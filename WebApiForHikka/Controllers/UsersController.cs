using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiForHikka.Application.Users;
using WebApiForHikka.Constants.AppSettings;
using WebApiForHikka.Constants.Shared;
using WebApiForHikka.Constants.Users;
using WebApiForHikka.Domain;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.Users;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.Controllers;
public class UsersController : MyBaseController, ICrudController<UpdateUserDto, UserRegistrationDto>
{
    private readonly IUserService _userService; 
    private readonly IConfiguration _configuration;

    public UsersController(IUserService userService, IConfiguration configuration, IMapper mapper, IHttpContextAccessor httpContextAccessor) :base(mapper, httpContextAccessor)
    {
        _userService = userService;
        _configuration = configuration;
    }

    [HttpPost("Registrate")]
    public async Task<IActionResult> Create([FromBody] UserRegistrationDto model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(GetAllErrorsDuringValidation());
        }

        var user = new User(model.Password, model.Email, model.Role);

        var id = await _userService.RegisterUserAsync(user, cancellationToken);

        if (id == null)
        {
            return BadRequest(UserStringConstants.MessageUserIsntRegistrated);
        }

        return Ok(new RegistratedResponseUserDto() {  Message = UserStringConstants.MessageUserRegistrated, Id = (Guid)id });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]UserLoginDto model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(GetAllErrorsDuringValidation());
        }

        var user = await _userService.AuthenticateUserAsync(model.Email, model.Password, cancellationToken);
        if (user == null) return Unauthorized();

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration[AppSettingsStringConstants.JwtKey]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(UserStringConstants.EmailClaim, user.Email),
                new Claim(UserStringConstants.RoleClaim, user.Role),
                new Claim(UserStringConstants.IdClaim, user.Id.ToString()),
            }),
            Expires = DateTime.UtcNow.AddDays(ShraredNumberConstatnts.HowManyDayExpiresForJwt),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return Ok(new LoginResponseUserDto() { Token = tokenString });
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll([FromQuery] FilterPaginationDto paginationDto, CancellationToken cancellationToken)
    {
        var paginationCollection = await _userService.GetAllAsync(paginationDto, cancellationToken);

        var users = _mapper.Map<List<GetUserDto>>(paginationCollection.Models);
        return Ok(
            new ReturnUserPageDto()
            {
                HowManyPages = (int)Math.Ceiling((double)paginationCollection.Total/paginationDto.PageSize),
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
        var user = _mapper.Map<User>(dto);
        if (!ModelState.IsValid)
        {
            return BadRequest(GetAllErrorsDuringValidation());
        }

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

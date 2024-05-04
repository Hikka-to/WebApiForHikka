using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using WebApiForHikka.Application.Users;
using WebApiForHikka.Constants.AppSettings;
using WebApiForHikka.Constants.Users;
using WebApiForHikka.Domain;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.WebApi.Dto.Users;
using WebApiForHikka.WebApi.Helper.HashFunction;

namespace WebApiForHikka.WebApi.Controllers;
public class UsersController : Controller
{
    private readonly IUserService _userService; 
    private readonly IHashFunctions _hashFunctions; 
    private readonly IConfiguration _configuration;

    public UsersController(IUserService userService, IConfiguration configuration, IHashFunctions hashFunctions, IMapper mapper, IHttpContextAccessor httpContextAccessor) :base(mapper, httpContextAccessor)
    {
        _userService = userService;
        _configuration = configuration;
        _hashFunctions = hashFunctions;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegistrationDto model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(GetAllErrorsDuringValidation());
        }
        
        var user = new User(_hashFunctions.HashPassword(model.Password), model.Email, model.Role);

        var id = await _userService.RegisterUserAsync(user, cancellationToken);
        return Ok(new { message = "User created successfully", id=id });
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
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return Ok(new { token = tokenString });
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
                Users = users,
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

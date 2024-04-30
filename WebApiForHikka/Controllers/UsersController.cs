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
using WebApiForHikka.Domain.Models;
using WebApiForHikka.WebApi.Dto.Users;
using WebApiForHikka.WebApi.Helper.HashFunction;

namespace WebApiForHikka.WebApi.Controllers;
public class UsersController : Controller
{
    private readonly IUserService _userService; 
    private readonly IHashFunctions _hashFunctions; 
    private readonly IConfiguration _configuration;

    public UsersController(IUserService userService, IConfiguration configuration, IHashFunctions hashFunctions, IMapper mapper) :base(mapper)
    {
        _userService = userService;
        _configuration = configuration;
        _hashFunctions = hashFunctions;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegistrationDto model, CancellationToken cancellationToken)
    {
        var user = new User(_hashFunctions.HashPassword(model.Password), model.Email, model.Role);

        var id = await _userService.RegisterUserAsync(user, cancellationToken);
        return Ok(new { message = "User created successfully", id=id });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDto model, CancellationToken cancellationToken)
    {
        var user = await _userService.AuthenticateUserAsync(model.Email, model.Password, cancellationToken);
        if (user == null) return Unauthorized();

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration[AppSettingsStringConstants.JwtKey]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return Ok(new { token = tokenString });
    }
}

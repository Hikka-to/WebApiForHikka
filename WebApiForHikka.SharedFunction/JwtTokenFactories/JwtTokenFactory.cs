using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiForHikka.Constants.AppSettings;
using WebApiForHikka.Constants.Models.Users;
using WebApiForHikka.Constants.Shared;
using WebApiForHikka.Domain.Models;

namespace WebApiForHikka.SharedFunction.JwtTokenFactories;

public class JwtTokenFactory : IJwtTokenFactory
{
    public string? GetJwtToken(User user, IConfiguration configuration)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(configuration[AppSettingsStringConstants.JwtKey]!);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(UserStringConstants.EmailClaim, user.Email),
                new(UserStringConstants.RoleClaim, user.Role),
                new(UserStringConstants.IdClaim, user.Id.ToString()),
            }),
            Expires = DateTime.UtcNow.AddDays(ShraredNumberConstatnts.HowManyDayExpiresForJwt),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return tokenString;
    }
}

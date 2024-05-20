using Microsoft.Extensions.Configuration;
using System.Reflection.Metadata;
using WebApiForHikka.Domain.Models;

namespace WebApiForHikka.SharedFunction.JwtTokenFactories;

public interface IJwtTokenFactory
{
    public string? GetJwtToken(User user, IConfiguration configuration);

}

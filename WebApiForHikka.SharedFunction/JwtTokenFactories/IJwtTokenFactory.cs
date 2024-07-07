using Microsoft.Extensions.Configuration;
using WebApiForHikka.Domain.Models;

namespace WebApiForHikka.SharedFunction.JwtTokenFactories;

public interface IJwtTokenFactory
{
    public Task<string?> GetJwtTokenAsync(User user, IConfiguration configuration);
}
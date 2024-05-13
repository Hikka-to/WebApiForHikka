using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using WebApiForHikka.Constants.Users;
using WebApiForHikka.Dtos.Dto.Authorization;

namespace WebApiForHikka.WebApi.Shared;

[Route("api/[controller]")]
public abstract class MyBaseController : ControllerBase
{
    protected readonly IMapper _mapper;
    protected readonly IHttpContextAccessor _httpContextAccessor;

    protected MyBaseController(IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    protected JwtTokenContentDto GetJwtTokenAuthorizationFromHeader()
    {
        var authHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
        if (authHeader != null)
        {
            try
            {


                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(authHeader);
                string? userEmail = jwtToken.Payload.FirstOrDefault(c => c.Key == UserStringConstants.EmailClaim).Value.ToString();
                string? userRole = jwtToken.Payload.FirstOrDefault(c => c.Key == UserStringConstants.RoleClaim).Value.ToString();
                string? userId = jwtToken.Payload.FirstOrDefault(c => c.Key == UserStringConstants.IdClaim).Value.ToString();

                return new JwtTokenContentDto() { Email = userEmail, Role = userRole, Id = userId };
            }
            catch (Exception)
            {

                return new JwtTokenContentDto()
                {
                    Email = null,
                    Role = null,
                    Id = null,
                };
            }
        }

        return new JwtTokenContentDto()
        {
            Email = null,
            Role = null,
            Id = null,
        };
    }

    protected bool CheckIfTheUserHasTheRightRole(JwtTokenContentDto jwtTokenContent, string[] roles)
    {
        for (int i = 0; i < roles.Length; ++i)
        {
            roles[i] = roles[i].ToLower();
        }
        if (roles.Contains(jwtTokenContent.Role?.ToLower()))
        {
            return true;
        }
        return false;
    }

    protected IEnumerable<string> GetAllErrorsDuringValidation()
    {
        return ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage);
    }


}

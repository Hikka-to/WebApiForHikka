using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using WebApiForHikka.Constants.Controllers;
using WebApiForHikka.Constants.Models.Users;
using WebApiForHikka.Dtos.Dto.Authorization;
using WebApiForHikka.WebApi.Shared.ErrorEndPoints;

namespace WebApiForHikka.WebApi.Shared;

[Route("api/[controller]")]
public abstract class MyBaseController
    (IMapper mapper, IHttpContextAccessor httpContextAccessor)
    : ControllerBase
{
    protected readonly IMapper _mapper = mapper;
    protected readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    protected JwtTokenContentDto GetJwtTokenAuthorizationFromHeader()
    {
        var authHeader = _httpContextAccessor.HttpContext!.Request.Headers.Authorization;

        var headerString = authHeader.ToString();

        if (headerString != null)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(headerString);
                string? userEmail = jwtToken.Payload.FirstOrDefault(c => c.Key == UserStringConstants.EmailClaim).Value.ToString();
                string? userRole = jwtToken.Payload.FirstOrDefault(c => c.Key == UserStringConstants.RoleClaim).Value.ToString();
                string? userId = jwtToken.Payload.FirstOrDefault(c => c.Key == UserStringConstants.IdClaim).Value.ToString();

                return new JwtTokenContentDto() { Email = userEmail, Role = UserStringConstants.UserRole, Id = userId };
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

    protected virtual ErrorEndPoint ValidateRequest(ThingsToValidateBase thingsToValidate)
    {
        ErrorEndPoint errorEndPoint = new();

        var jwt = GetJwtTokenAuthorizationFromHeader();
        if (!CheckIfTheUserHasTheRightRole(jwt, thingsToValidate.RolesToAccessTheEndPoint))
        {
            string errorMessage = ControllerStringConstants.ErrorMessageThisEndpointCanAccess
                + string.Join(", ", thingsToValidate.RolesToAccessTheEndPoint);
            errorEndPoint.UnauthorizedObjectResult = Unauthorized(errorMessage);
            return errorEndPoint;
        }
        if (!ModelState.IsValid)
        {
            errorEndPoint.BadRequestObjectResult = BadRequest(GetAllErrorsDuringValidation());
            return errorEndPoint;
        }
        return errorEndPoint;
    }

    protected IEnumerable<string> GetAllErrorsDuringValidation()
    {
        return ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage);
    }

    protected record ThingsToValidateBase
    {
        public required string[] RolesToAccessTheEndPoint { get; init; }
    }
}
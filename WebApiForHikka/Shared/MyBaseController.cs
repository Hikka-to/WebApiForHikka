﻿using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Constants.Models.Users;
using WebApiForHikka.Dtos.Dto.Authorization;
using WebApiForHikka.WebApi.Shared.ErrorEndPoints;

namespace WebApiForHikka.WebApi.Shared;

[Route("api/v1/[controller]")]
public abstract class MyBaseController(IMapper mapper, IHttpContextAccessor httpContextAccessor)
    : ControllerBase
{
    protected readonly IHttpContextAccessor HttpContextAccessor = httpContextAccessor;
    protected readonly IMapper Mapper = mapper;


    protected JwtTokenContentDto GetJwtTokenAuthorizationFromHeader()
    {
        var authHeader = HttpContextAccessor.HttpContext!.Request.Headers.Authorization;

        var headerString = authHeader.ToString();

        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (headerString == null)
            return new JwtTokenContentDto
            {
                Email = null,
                Role = null,
                Id = null
            };
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(headerString);
            var userEmail = jwtToken.Payload.FirstOrDefault(c => c.Key == UserStringConstants.EmailClaim).Value
                .ToString();
            var userId = jwtToken.Payload.FirstOrDefault(c => c.Key == UserStringConstants.IdClaim).Value
                .ToString();

            return new JwtTokenContentDto { Email = userEmail, Role = UserStringConstants.UserRole, Id = userId };
        }
        catch (Exception)
        {
            return new JwtTokenContentDto
            {
                Email = null,
                Role = null,
                Id = null
            };
        }
    }


    protected virtual ErrorEndPoint ValidateRequest(ThingsToValidateBase thingsToValidate)
    {
        ErrorEndPoint errorEndPoint = new();

        if (ModelState.IsValid) return errorEndPoint;
        errorEndPoint.BadRequestObjectResult = new BadRequestObjectResult(GetAllErrorsDuringValidation())
            { StatusCode = 422 };
        return errorEndPoint;
    }

    protected IDictionary<string, IEnumerable<string>> GetAllErrorsDuringValidation()
    {
        return ModelState.Where(kvp => kvp.Value?.Errors.Any() ?? false).ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage)
        );
    }

    protected record ThingsToValidateBase
    {
        

    }
}
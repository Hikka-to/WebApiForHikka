using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WebApiForHikka.Constants.Users;


namespace WebApiForHikka.Dtos.Controllers;

public class DummyController : Controller
{

    public DummyController(IMapper mapper,IHttpContextAccessor httpContextAccessor) : base(mapper, httpContextAccessor )
    {
    }

    [HttpGet("EndPointForAdmin")]
    public IActionResult EndPointForAdmin()
    {
        var jwt = this.GetJwtTokenAuthorizationFromHeader();
        if (this.CheckIfTheUserHasTheRightRole(jwt, [UserStringConstants.AdminRole]))
        {
            return Ok("You are admin");
        }
        else 
        {
            return Unauthorized("this endpoint is only for admin user");
        }
    }

    [HttpGet("EndPointForUser")]
    public IActionResult EndPointForUser()
    {
        var jwt = this.GetJwtTokenAuthorizationFromHeader();
        if (this.CheckIfTheUserHasTheRightRole(jwt, [UserStringConstants.UserRole]))
        {
            return Ok("You are a user");
        }
        else 
        {
            return Unauthorized("this endpoint is only for user user");
        }

    }

    [HttpGet("EndPointForUserOrAdmin")]
    public IActionResult EndPointForUserOrAdmin()
    {
        var jwt = this.GetJwtTokenAuthorizationFromHeader();
        if (this.CheckIfTheUserHasTheRightRole(jwt, [UserStringConstants.UserRole, UserStringConstants.AdminRole]))
        {
            return Ok("You are a user or admin");
        }
        else 
        {
            return Unauthorized("this endpoint is only for user user");
        }

    }
}

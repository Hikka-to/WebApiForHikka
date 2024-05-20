using Microsoft.AspNetCore.Mvc;

namespace WebApiForHikka.WebApi.Shared.ErrorEndPoints;

public class ErrorEndPoint
{
    private UnauthorizedObjectResult? _unauthorizedObjectResult;
    private BadRequestObjectResult? _badRequestObjectResult;

    public UnauthorizedObjectResult? UnauthorizedObjectResult
    {
        get { return _unauthorizedObjectResult; }
        set
        {
            IsError = true;
            _unauthorizedObjectResult = value;
        }
    }
    public BadRequestObjectResult? BadRequestObjectResult
    {
        get { return _badRequestObjectResult; }
        set
        {
            IsError = true;
            _badRequestObjectResult = value;
        }
    }
    public bool IsError { private set; get; }

    public ErrorEndPoint()
    {
        IsError = false;
    }

    public IActionResult GetError()
    {
        if (!IsError) return new BadRequestObjectResult("There is no error if you see the message please contact the developer");

        if (BadRequestObjectResult != null) return _badRequestObjectResult!;

        if (UnauthorizedObjectResult != null) return _unauthorizedObjectResult!;

        return new BadRequestObjectResult("There is no error if you see the message please contact the developer");
    }
}
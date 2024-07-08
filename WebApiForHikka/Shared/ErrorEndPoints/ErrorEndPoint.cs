using Microsoft.AspNetCore.Mvc;

namespace WebApiForHikka.WebApi.Shared.ErrorEndPoints;

public class ErrorEndPoint
{
    private BadRequestObjectResult? _badRequestObjectResult;
    private UnauthorizedObjectResult? _unauthorizedObjectResult;

    public ErrorEndPoint()
    {
        IsError = false;
    }

    public UnauthorizedObjectResult? UnauthorizedObjectResult
    {
        get => _unauthorizedObjectResult;
        set
        {
            IsError = true;
            _unauthorizedObjectResult = value;
        }
    }

    public BadRequestObjectResult? BadRequestObjectResult
    {
        get => _badRequestObjectResult;
        set
        {
            IsError = true;
            _badRequestObjectResult = value;
        }
    }

    public bool IsError { private set; get; }

    public IActionResult GetError()
    {
        if (!IsError)
            return new BadRequestObjectResult("There is no error if you see the message please contact the developer");

        if (BadRequestObjectResult != null) return _badRequestObjectResult!;

        if (UnauthorizedObjectResult != null) return _unauthorizedObjectResult!;

        return new BadRequestObjectResult("There is no error if you see the message please contact the developer");
    }
}
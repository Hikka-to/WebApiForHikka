using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace WebApiForHikka.WebApi.Controllers;

[Route("api/[controller]")]
public abstract class Controller : ControllerBase
{
    protected readonly IMapper _mapper;

    protected Controller(IMapper mapper) 
    {
        this._mapper = mapper;
    }
}

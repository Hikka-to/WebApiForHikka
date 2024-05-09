using AutoMapper;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using WebApiForHikka.Domain;

namespace WebApiForHikka.Test.Controller.Shared;

public class BaseControllerTest
{
    protected readonly IMapper _mapper; 
    protected readonly IHttpContextAccessor _httpContextAccessor;

    protected CancellationToken _cancellationToken => new CancellationToken(); 
    protected FilterPaginationDto _filterPaginationDto => new FilterPaginationDto(); 

    public BaseControllerTest() 
    {
        _mapper = A.Fake<IMapper>();
        _httpContextAccessor = A.Fake<IHttpContextAccessor>();
    } 


}

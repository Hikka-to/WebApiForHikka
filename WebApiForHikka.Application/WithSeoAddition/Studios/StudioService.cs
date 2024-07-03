using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.WithSeoAddition.Studios;

public class StudioService(IStudioRepository repository) : CrudService<Studio, IStudioRepository>(repository),
    IStudioService;

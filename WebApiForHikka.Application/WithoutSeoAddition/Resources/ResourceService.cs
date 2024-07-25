using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Application.WithoutSeoAddition.Resources;

public class ResourceService (IResourceRepository repository)
    : CrudService<Resource, IResourceRepository>(repository), IResourceService;

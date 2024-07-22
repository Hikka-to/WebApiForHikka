using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.WithoutSeoAddition.Providers;

public class ProviderService(IProviderRepository repository)
    : CrudService<Provider, IProviderRepository>(repository), IProviderService
{
}
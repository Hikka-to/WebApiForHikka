using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Application.WithoutSeoAddition.ExternalLinks;

public class ExternalLinkService(IExternalLinkRepository repository)
    : CrudService<ExternalLink, IExternalLinkRepository>(repository), IExternalLinkService;
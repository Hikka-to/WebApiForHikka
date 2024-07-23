using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.WithSeoAddition.Sources;

public class SourceService(ISourceRepository repository)
    : CrudService<Source, ISourceRepository>(repository), ISourceService;
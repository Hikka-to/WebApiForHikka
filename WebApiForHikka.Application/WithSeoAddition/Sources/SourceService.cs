using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models;

namespace WebApiForHikka.Application.Sources;

public class SourceService(ISourceRepository repository)
    : CrudService<Source, ISourceRepository>(repository), ISourceService;
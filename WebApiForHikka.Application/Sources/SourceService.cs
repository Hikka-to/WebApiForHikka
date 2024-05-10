using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models;

namespace WebApiForHikka.Application.Sources;

public class SourceService : CrudService<Source, ISourceRepository>, ISourceService
{
    public SourceService(ISourceRepository repository) : base(repository)
    {
    }
}

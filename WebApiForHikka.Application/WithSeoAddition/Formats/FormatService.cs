using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models;

namespace WebApiForHikka.Application.Formats;
public class FormatService : CrudService<Format, IFormatRepository>, IFormatService
{
    public FormatService(IFormatRepository repository) : base(repository)
    {
    }
}

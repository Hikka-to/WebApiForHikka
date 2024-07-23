using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.WithSeoAddition.Formats;

public class FormatService(IFormatRepository repository)
    : CrudService<Format, IFormatRepository>(repository), IFormatService;
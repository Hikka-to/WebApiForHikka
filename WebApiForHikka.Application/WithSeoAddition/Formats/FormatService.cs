using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models;

namespace WebApiForHikka.Application.Formats;

public class FormatService(IFormatRepository repository)
    : CrudService<Format, IFormatRepository>(repository), IFormatService;
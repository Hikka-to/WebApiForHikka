using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Application.WithoutSeoAddition.AlternativeNames;

public class AlternativeNameService(IAlternativeNameRepository repository)
    : CrudService<AlternativeName, IAlternativeNameRepository>(repository), IAlternativeNameService;
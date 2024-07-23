using WebApiForHikka.Application.Kinds;
using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.WithSeoAddition.Languages;

public class LanguageService(ILanguageRepository repository) : CrudService<Language, ILanguageRepository>(repository), ILanguageService;

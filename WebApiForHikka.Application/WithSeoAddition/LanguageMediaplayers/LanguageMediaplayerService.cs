using WebApiForHikka.Application.Shared;
using WebApiForHikka.Application.WithSeoAddition.Languages;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.LanguageMediaplayers;

public class LanguageMediaplayerService (ILanguageMediaplayerRepository repository) : CrudService<LanguageMediaplayer, ILanguageMediaplayerRepository>(repository), ILanguageMediaplayerService;

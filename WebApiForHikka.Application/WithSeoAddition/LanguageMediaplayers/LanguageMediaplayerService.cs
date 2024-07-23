using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.WithSeoAddition.LanguageMediaplayers;

public class LanguageMediaplayerService(ILanguageMediaplayerRepository repository)
    : CrudService<LanguageMediaplayer, ILanguageMediaplayerRepository>(repository), ILanguageMediaplayerService;
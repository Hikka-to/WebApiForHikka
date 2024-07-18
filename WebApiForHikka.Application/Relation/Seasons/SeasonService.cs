using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Application.Relation.Seasons;

public class SeasonService(ISeasonRepository repository)
    : CrudService<Season, ISeasonRepository>(repository), ISeasonService;
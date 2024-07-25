using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.Relation.UserAnimeLists;

public class UserAnimeListRelationService(IUserAnimeListRelationRepository repository)
    : RelationCrudService<UserAnimeList, User, Anime, IUserAnimeListRelationRepository>(repository),
        IUserAnimeListRelationService;
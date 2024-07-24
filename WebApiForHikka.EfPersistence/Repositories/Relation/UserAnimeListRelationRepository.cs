using WebApiForHikka.Application.Relation.UserAnimeLists;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.Relation;

public class UserAnimeListRelationRepository(HikkaDbContext dbContext)
    : CrudRelationRepository<UserAnimeList, User, Anime>(dbContext), IUserAnimeListRelationRepository;
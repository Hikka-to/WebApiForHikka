using WebApiForHikka.Application.Relation.UserAnimeListTypes;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;

public class UserAnimeListTypeRepository(HikkaDbContext dbContext)
    : CrudRepository<UserAnimeListType>(dbContext), IUserAnimeListTypeRepository
{
}
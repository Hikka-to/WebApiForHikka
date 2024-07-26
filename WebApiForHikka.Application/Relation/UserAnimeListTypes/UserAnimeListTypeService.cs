using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Application.Relation.UserAnimeListTypes;

public class UserAnimeListTypeService(IUserAnimeListTypeRepository repository)
    : CrudService<UserAnimeListType, IUserAnimeListTypeRepository>(repository), IUserAnimeListTypeService
{
}
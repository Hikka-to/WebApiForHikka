using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Application.WithoutSeoAddition.UserAnimeListTypes;

public class UserAnimeListTypeService(IUserAnimeListTypeRepository repository)
    : CrudService<UserAnimeListType, IUserAnimeListTypeRepository>(repository), IUserAnimeListTypeService
{
    
}
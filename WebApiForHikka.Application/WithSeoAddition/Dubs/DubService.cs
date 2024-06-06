using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.WithSeoAddition.Dubs;

public class DubService(IDubRepository repository) : CrudService<Dub, IDubRepository>(repository), IDubService;

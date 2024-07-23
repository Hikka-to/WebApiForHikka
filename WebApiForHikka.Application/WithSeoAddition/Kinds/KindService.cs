using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.WithSeoAddition.Kinds;

public class KindService(IKindRepository repository) : CrudService<Kind, IKindRepository>(repository), IKindService;
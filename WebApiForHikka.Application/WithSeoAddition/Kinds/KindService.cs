using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models;

namespace WebApiForHikka.Application.Kinds;
public class KindService(IKindRepository repository) : CrudService<Kind, IKindRepository>(repository), IKindService;
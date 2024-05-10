using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models;

namespace WebApiForHikka.Application.Kinds;
public class KindService : CrudService<Kind, IKindRepository>, IKindService
{
    public KindService(IKindRepository repository) : base(repository)
    {
    }
}

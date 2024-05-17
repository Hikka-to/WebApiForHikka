using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models;

namespace WebApiForHikka.Application.Statuses;

public interface IStatusRepository : ICrudRepository<Status>
{
}

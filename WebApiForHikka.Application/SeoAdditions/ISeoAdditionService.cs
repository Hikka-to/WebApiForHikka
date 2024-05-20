using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models;

namespace WebApiForHikka.Application.SeoAdditions;
public interface ISeoAdditionService : ICrudService<SeoAddition>
{
    public bool CheckIfTheSeoAdditionExist(Guid id);
}
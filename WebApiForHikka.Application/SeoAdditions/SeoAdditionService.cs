using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models;

namespace WebApiForHikka.Application.SeoAdditions;

public class SeoAdditionService : CrudService<SeoAddition, ISeoAdditionRepository>, ISeoAdditionService
{
    public SeoAdditionService(ISeoAdditionRepository repository) : base(repository)
    {
    }

    public bool CheckIfTheSeoAdditionExist(Guid id)
    {
        return Repository.CheckIfTheSeoAdditionExist(id);
    }
}
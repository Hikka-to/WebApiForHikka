using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Constants.Models.SeoAdditions;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories;

public class SeoAdditionRepository(HikkaDbContext dbContext) : CrudRepository<SeoAddition>(dbContext), ISeoAdditionRepository
{
    public bool CheckIfTheSeoAdditionExist(Guid id)
    {
        var user = DbContext.Set<SeoAddition>().FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return false;
        }
        return true;
    }
}

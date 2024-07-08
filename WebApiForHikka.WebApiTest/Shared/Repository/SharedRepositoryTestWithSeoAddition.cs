using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;

namespace WebApiForHikka.Test.Shared.Repository;

public abstract class SharedRepositoryTestWithSeoAddition<TModel, TRepository>
    : SharedRepositoryTest<TModel, TRepository>
    where TModel : ModelWithSeoAddition
    where TRepository : ICrudRepository<TModel>
{
    protected SeoAddition GetSeoAdditionSample()
    {
        return GetSeoAdditionModels.GetSample();
    }

    protected SeoAddition GetSeoAdditionSampleUpdate()
    {
        return GetSeoAdditionModels.GetSampleForUpdate();
    }
}
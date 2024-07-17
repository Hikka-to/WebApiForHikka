using WebApiForHikka.Application.WithoutSeoAddition.RelatedTypes;
using WebApiForHikka.Constants.Models.RelatedTypes;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithoutSeoAddition.RelatedTypes;

public class RelatedTypeServiceTest : SharedServiceTest<RelatedType, RelatedTypeService>
{
    protected override RelatedType GetSample()
    {
        return GetRelatedTypeModel.GetSample();
    }

    protected override RelatedType GetSampleForUpdate()
    {
        return GetRelatedTypeModel.GetSampleForUpdate();
    }

    protected override RelatedTypeService GetService(HikkaDbContext hikkaDbContext)
    {
        var repostiory = new RelatedTypeRepository(hikkaDbContext);
        return new RelatedTypeService(repostiory);
    }
}

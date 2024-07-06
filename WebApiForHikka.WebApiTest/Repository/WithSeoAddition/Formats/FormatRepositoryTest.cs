using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Shared.Models.WithSeoAddtion;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithSeoAddition.Formats;

public class FormatRepositoryTest : SharedRepositoryTestWithSeoAddition<
    Format,
    FormatRepository
    >
{
    protected override FormatRepository GetRepository(HikkaDbContext hikkaDbContext) =>
        new(hikkaDbContext);

    protected override Format GetSample() => GetFormatModels.GetSample();
    protected override Format GetSampleForUpdate() => GetFormatModels.GetSampleForUpdate();  
        
  }
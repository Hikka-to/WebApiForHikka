using WebApiForHikka.Application.Sources;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.Sources;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition;

namespace WebApiForHikka.Test.Controllers.CrudControllers.WithSeoAddition;

public class SourceControllerTest : CrudControllerBaseWithSeoAddition<
    SourceController,
    SourceService,
    Source,
    ISourceRepository,
    UpdateSourceDto,
    CreateSourceDto,
    GetSourceDto,
    ReturnPageDto<GetSourceDto>
    >
{
    protected override AllServicesInControllerWithSeoAddition GetAllServices()
    {
        var dbContext = GetDatabaseContext();

        var seoAdditionRepository = new SeoAdditionRepository(dbContext);
        var formatRepository = new SourceRepository(dbContext);

        return new AllServicesInControllerWithSeoAddition(new SourceService(formatRepository), new SeoAdditionService(seoAdditionRepository));
    }



    protected override SourceController GetController(AllServicesInController allServicesInController)
    {
        AllServicesInControllerWithSeoAddition allServices = allServicesInController as AllServicesInControllerWithSeoAddition ?? throw new Exception("method getController in SourceControllerTest");

        return new SourceController(
            allServices.CrudService,
            allServices.SeoAdditionService,
            _mapper,
            GetHttpContextAccessForAdminUser()
            );
    }


    protected override CreateSourceDto GetCreateDtoSample()
    {
        return new CreateSourceDto()
        {
            Name = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionCreateDtoSample(),
        };
    }

    protected override GetSourceDto GetGetDtoSample()
    {
        return new GetSourceDto()
        {
            Name = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionGetDtoSample(),
            Id = new Guid(),
        };
    }

    protected override Source GetModelSample()
    {
        return new Source()
        {
            Name = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionSample(),
            Id = new Guid(),
        };
    }

    protected override UpdateSourceDto GetUpdateDtoSample()
    {
        return new UpdateSourceDto()
        {
            Name = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAddtionUpdateDtoSample(),
            Id = new Guid(),
        };
    }
}
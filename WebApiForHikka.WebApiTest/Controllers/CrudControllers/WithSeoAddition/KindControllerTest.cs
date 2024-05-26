using WebApiForHikka.Application.Kinds;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.Kinds;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition;

namespace WebApiForHikka.Test.Controllers.CrudControllers.WithSeoAddition;

public class KindControllerTest : CrudControllerBaseWithSeoAddition<
    KindController,
    KindService,
    Kind,
    IKindRepository,
    UpdateKindDto,
    CreateKindDto,
    GetKindDto,
    ReturnPageDto<GetKindDto>
    >
{
    protected override AllServicesInControllerWithSeoAddition GetAllServices()
    {
        var dbContext = GetDatabaseContext();

        var seoAdditionRepository = new SeoAdditionRepository(dbContext);
        var formatRepository = new KindRepository(dbContext);

        return new AllServicesInControllerWithSeoAddition(new KindService(formatRepository), new SeoAdditionService(seoAdditionRepository));
    }



    protected override KindController GetController(AllServicesInController allServicesInController)
    {
        AllServicesInControllerWithSeoAddition allServices = allServicesInController as AllServicesInControllerWithSeoAddition ?? throw new Exception("method getController in KindControllerTest");

        return new KindController(
            allServices.CrudService,
            allServices.SeoAdditionService,
            _mapper,
            GetHttpContextAccessForAdminUser()
            );
    }


    protected override CreateKindDto GetCreateDtoSample()
    {
        return new CreateKindDto()
        {
            Hint = Faker.Lorem.GetFirstWord(),
            Slug = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionCreateDtoSample(),
        };
    }

    protected override GetKindDto GetGetDtoSample()
    {
        return new GetKindDto()
        {
            Hint = Faker.Lorem.GetFirstWord(),
            Slug = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionGetDtoSample(),
            Id = new Guid(),
        };
    }

    protected override Kind GetModelSample()
    {
        return new Kind()
        {
            Hint = Faker.Lorem.GetFirstWord(),
            Slug = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionSample(),
            Id = new Guid(),
        };
    }

    protected override UpdateKindDto GetUpdateDtoSample()
    {
        return new UpdateKindDto()
        {
            Hint = Faker.Lorem.GetFirstWord(),
            Slug = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAddtionUpdateDtoSample(),
            Id = new Guid(),
        };
    }
}
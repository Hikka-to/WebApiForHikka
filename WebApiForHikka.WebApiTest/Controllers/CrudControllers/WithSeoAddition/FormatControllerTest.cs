using WebApiForHikka.Application.Formats;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.Formats;
using WebApiForHikka.Dtos.Dto.SeoAddition;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Controller.Shared;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers;
using WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition;

namespace WebApiForHikka.Test.Controllers.CrudControllers.WithSeoAddition;

public class FormatControllerTest : CrudControllerBaseWithSeoAddition<
    FormatController,
    FormatService,
    Format,
    IFormatRepository,
    UpdateFormatDto,
    CreateFormatDto,
    GetFormatDto,
    ReturnPageDto<GetFormatDto>
    >
{
    protected override AllServicesInControllerWithSeoAddition GetAllServices()
    {
        var dbContext = GetDatabaseContext();

         var seoAdditionRepository = new SeoAdditionRepository(dbContext);
         var formatRepository = new FormatRepository(dbContext);

        return new AllServicesInControllerWithSeoAddition( new FormatService(formatRepository), new SeoAdditionService(seoAdditionRepository));
    }

    protected override ICollection<Format> GetCollectionOfModels(int howMany)
    {
        ICollection<Format> seoAdditions = new List<Format>();
        for (int i = 0; i < howMany; ++i)
        {
            seoAdditions.Add(GetModelSample());
        }
        return seoAdditions;

    }

    protected override FormatController GetController(AllServicesInController allServicesInController)
    {
        AllServicesInControllerWithSeoAddition allServices = allServicesInController as AllServicesInControllerWithSeoAddition ?? throw new Exception("method getController in FormatControllerTest");

        return new FormatController(
            allServices.CrudService,
            allServices.SeoAdditionService,
            _mapper,
            GetHttpContextAccessForAdminUser()
            );
    }

   
    protected override CreateFormatDto GetCreateDtoSample()
    {
        return new CreateFormatDto()
        {
            Name = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionCreateDtoSample(),
        };
    }

    protected override GetFormatDto GetGetDtoSample()
    {
        return new GetFormatDto()
        {
            Name = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionGetDtoSample(),
            Id = new Guid(),
        };
    }

    protected override Format GetModelSample()
    {
        return new Format()
        {
            Name = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionSample(),
            Id = new Guid(),
        };
    }

    protected override UpdateFormatDto GetUpdateDtoSample()
    {
        return new UpdateFormatDto()
        {
            Name = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAddtionUpdateDtoSample(),
            Id = new Guid(),
        };
    }
}

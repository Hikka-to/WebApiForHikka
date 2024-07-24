using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.WithSeoAddition.Formats;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Formats;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.Test.Controllers.Shared;
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
    protected override AllServicesInControllerWithSeoAddition GetAllServices(IServiceCollection alternativeServices)
    {
        var dbContext = GetDatabaseContext();

        var seoAdditionRepository = new SeoAdditionRepository(dbContext);
        var formatRepository = new FormatRepository(dbContext);
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);

        return new AllServicesInControllerWithSeoAddition(new FormatService(formatRepository),
            new SeoAdditionService(seoAdditionRepository), userManager, roleManager);
    }

    protected override ICollection<Format> GetCollectionOfModels(int howMany)
    {
        ICollection<Format> seoAdditions = new List<Format>();
        for (var i = 0; i < howMany; ++i) seoAdditions.Add(GetModelSample());
        return seoAdditions;
    }

    protected override async Task<FormatController> GetController(AllServicesInController allServicesInController,
        IServiceProvider alternativeServices)
    {
        var allServices = allServicesInController as AllServicesInControllerWithSeoAddition ??
                          throw new Exception("method getController in FormatControllerTest");

        return new FormatController(
            allServices.CrudService,
            allServices.SeoAdditionService,
            _mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager, allServices.RoleManager)
        );
    }


    protected override CreateFormatDto GetCreateDtoSample()
    {
        return GetFormatModels.GetCreateDtoSample();
    }

    protected override GetFormatDto GetGetDtoSample()
    {
        return GetFormatModels.GetGetDtoSample();
    }

    protected override Format GetModelSample()
    {
        return GetFormatModels.GetModelSample();
    }

    protected override UpdateFormatDto GetUpdateDtoSample()
    {
        return GetFormatModels.GetUpdateDtoSample();
    }
}
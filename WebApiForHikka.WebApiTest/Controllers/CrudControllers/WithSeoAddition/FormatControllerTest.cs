using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.Formats;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.Formats;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.Test.Shared.Models.WithSeoAddtion;
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

        return new AllServicesInControllerWithSeoAddition(new FormatService(formatRepository), new SeoAdditionService(seoAdditionRepository), userManager, roleManager);
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

    protected override async Task<FormatController> GetController(AllServicesInController allServicesInController, IServiceProvider alternativeServices)
    {
        AllServicesInControllerWithSeoAddition allServices = allServicesInController as AllServicesInControllerWithSeoAddition ?? throw new Exception("method getController in FormatControllerTest");

        return new FormatController(
            allServices.CrudService,
            allServices.SeoAdditionService,
            _mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager, allServices.RoleManager)
            );
    }


    protected override CreateFormatDto GetCreateDtoSample() => GetFormatModels.GetCreateDtoSample();
    protected override GetFormatDto GetGetDtoSample() => GetFormatModels.GetGetDtoSample();
    protected override Format GetModelSample() => GetFormatModels.GetModelSample();
    protected override UpdateFormatDto GetUpdateDtoSample()=> GetFormatModels.GetUpdateDtoSample();

}

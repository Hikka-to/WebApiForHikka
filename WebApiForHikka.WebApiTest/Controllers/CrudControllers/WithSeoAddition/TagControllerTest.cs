using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.WithSeoAddition.Tags;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Tags;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition;

namespace WebApiForHikka.Test.Controllers.CrudControllers.WithSeoAddition;

public class TagControllerTest : CrudControllerBaseWithSeoAddition<
    TagController,
    TagService,
    Tag,
    ITagRepository,
    UpdateTagDto,
    CreateTagDto,
    GetTagDto,
    ReturnPageDto<GetTagDto>
>
{
    protected override AllServicesInControllerWithSeoAddition GetAllServices(IServiceCollection alternativeServices)
    {
        var dbContext = GetDatabaseContext();

        var seoAdditionRepository = new SeoAdditionRepository(dbContext);
        var tagRepository = new TagRepository(dbContext);
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);

        return new AllServicesInControllerWithSeoAddition(new TagService(tagRepository),
            new SeoAdditionService(seoAdditionRepository), userManager, roleManager);
    }


    protected override async Task<TagController> GetController(AllServicesInController allServicesInController,
        IServiceProvider alternativeServices)
    {
        var allServices = allServicesInController as AllServicesInControllerWithSeoAddition ??
                          throw new Exception("method getController in TagControllerTest");

        return new TagController(
            allServices.CrudService,
            allServices.SeoAdditionService,
            Mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager,
                allServicesInController.RoleManager)
        );
    }


    protected override CreateTagDto GetCreateDtoSample()
    {
        return GetTagModels.GetCreateDtoSample();
    }

    protected override GetTagDto GetGetDtoSample()
    {
        return GetTagModels.GetGetDtoSample();
    }

    protected override Tag GetModelSample()
    {
        return GetTagModels.GetModelSample();
    }

    protected override UpdateTagDto GetUpdateDtoSample()
    {
        return GetTagModels.GetUpdateDtoSample();
    }
}
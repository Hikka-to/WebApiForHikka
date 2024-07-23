using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.WithoutSeoAddition.EmojiGroups;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.EmojiGroups;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

namespace WebApiForHikka.Test.Controllers.CrudControllers.WithoutSeoAddition;

public class EmojiGroupControllerTest
    : CrudControllerBaseTest<
        EmojiGroupController,
        EmojiGroupService,
        EmojiGroup,
        IEmojiGroupRepository,
        UpdateEmojiGroupDto,
        CreateEmojiGroupDto,
        GetEmojiGroupDto,
        ReturnPageDto<GetEmojiGroupDto>
    >
{
    protected override AllServicesInController GetAllServices(IServiceCollection alternativeServices)
    {
        var dbContext = GetDatabaseContext();

        var repository = new EmojiGroupRepository(dbContext);
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);

        return new AllServicesInController(new EmojiGroupService(repository), userManager, roleManager);
    }

    protected override async Task<EmojiGroupController> GetController(
        AllServicesInController allServicesInController, IServiceProvider alternativeServices)
    {
        var allServices = allServicesInController;

        return new EmojiGroupController(
            allServices.CrudService,
            _mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager,
                allServicesInController.RoleManager)
        );
    }

    protected override CreateEmojiGroupDto GetCreateDtoSample()
    {
        return GetEmojiGroupModels.GetCreateDtoSample();
    }

    protected override GetEmojiGroupDto GetGetDtoSample()
    {
        return GetEmojiGroupModels.GetGetDtoSample();
    }

    protected override UpdateEmojiGroupDto GetUpdateDtoSample()
    {
        return GetEmojiGroupModels.GetUpdateDtoSample();
    }

    protected override EmojiGroup GetModelSample()
    {
        return GetEmojiGroupModels.GetSample();
    }
}
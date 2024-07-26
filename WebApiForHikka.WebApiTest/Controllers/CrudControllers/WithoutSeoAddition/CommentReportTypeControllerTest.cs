using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.WithoutSeoAddition.CommentReportTypes;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.CommentReportTypes;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

namespace WebApiForHikka.Test.Controllers.CrudControllers.WithoutSeoAddition;

public class CommentReportTypeControllerTest : CrudControllerBaseTest<
    CommentReportTypeController,
    CommentReportTypeService,
    CommentReportType,
    ICommentReportTypeRepository,
    UpdateCommentReportTypeDto,
    CreateCommentReportTypeDto,
    GetCommentReportTypeDto,
    ReturnPageDto<GetCommentReportTypeDto>
>
{
    protected override AllServicesInController GetAllServices(IServiceCollection alternativeServices)
    {
        var dbContext = GetDatabaseContext();

        var repository = new CommentReportTypeRepository(dbContext);
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);

        return new AllServicesInController(new CommentReportTypeService(repository), userManager, roleManager);
    }

    protected override async Task<CommentReportTypeController> GetController(
        AllServicesInController allServicesInController, IServiceProvider alternativeServices)
    {
        var allServices = allServicesInController;

        return new CommentReportTypeController(
            allServices.CrudService,
            Mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager,
                allServicesInController.RoleManager)
        );
    }

    protected override CreateCommentReportTypeDto GetCreateDtoSample()
    {
        return GetCommentReportTypeModels.GetCreateDtoSample();
    }

    protected override GetCommentReportTypeDto GetGetDtoSample()
    {
        return GetCommentReportTypeModels.GetGetDtoSample();
    }

    protected override UpdateCommentReportTypeDto GetUpdateDtoSample()
    {
        return GetCommentReportTypeModels.GetUpdateDtoSample();
    }

    protected override CommentReportType GetModelSample()
    {
        return GetCommentReportTypeModels.GetSample();
    }
}
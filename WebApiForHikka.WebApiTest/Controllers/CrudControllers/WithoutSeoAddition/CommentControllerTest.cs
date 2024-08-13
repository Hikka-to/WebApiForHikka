using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.Commentables;
using WebApiForHikka.Application.Users;
using WebApiForHikka.Application.WithoutSeoAddition.Comments;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Comments;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.SharedFunction.Helpers.FileHelper;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

namespace WebApiForHikka.Test.Controllers.CrudControllers.WithoutSeoAddition;

public class CommentControllerTest : CrudControllerBaseTest<
    CommentController,
    CommentService,
    Comment,
    ICommentRepository,
    UpdateCommentDto,
    CreateCommentDto,
    GetCommentDto,
    ReturnPageDto<GetCommentDto>
>
{
    protected override AllServicesInController GetAllServices(IServiceCollection alternativeServices)
    {
        var dbContext = GetDatabaseContext();

        var repository = new CommentRepository(dbContext);
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);

        alternativeServices.AddSingleton(dbContext);
        alternativeServices.AddSingleton(userManager);
        alternativeServices.AddSingleton<IFileHelper, FileHelper>();
        alternativeServices.AddSingleton<IUserRepository, UserRepository>();
        alternativeServices.AddSingleton<IUserService, UserService>();
        alternativeServices.AddSingleton<ICommentableRepository, CommentableRepository>();
        alternativeServices.AddSingleton<ICommentableService, CommentableService>();

        return new AllServicesInController(new CommentService(repository), userManager, roleManager);
    }

    protected override async Task<CommentController> GetController(
        AllServicesInController allServicesInController, IServiceProvider alternativeServices)
    {
        return new CommentController(
            allServicesInController.CrudService,
            Mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager,
                allServicesInController.RoleManager),
            alternativeServices.GetRequiredService<IUserService>(),
            alternativeServices.GetRequiredService<ICommentableService>()
        );
    }

    protected override void MutationBeforeDtoCreation(CreateCommentDto createDto,
        AllServicesInController allServicesInController, IServiceProvider alternativeServices)
    {
        var user = SampleUser;
        var commentable = GetAnimeModels.GetSampleWithoutManyToMany();

        var commentableService = alternativeServices.GetRequiredService<ICommentableService>();

        commentableService.CreateAsync(commentable, default).Wait();

        createDto.UserId = user.Id;
        createDto.ParentId = commentable.Id;
    }

    protected override void MutationBeforeDtoUpdate(UpdateCommentDto updateDto,
        AllServicesInController allServicesInController,
        IServiceProvider alternativeServices)
    {
        var user = SampleUser;
        var commentable = GetAnimeModels.GetSampleWithoutManyToMany();

        var commentableService = alternativeServices.GetRequiredService<ICommentableService>();

        commentableService.CreateAsync(commentable, default).Wait();

        updateDto.UserId = user.Id;
        updateDto.ParentId = commentable.Id;
    }

    protected override CreateCommentDto GetCreateDtoSample()
    {
        return GetCommentModels.GetCreateDtoSample();
    }

    protected override GetCommentDto GetGetDtoSample()
    {
        return GetCommentModels.GetGetDtoSample();
    }

    protected override UpdateCommentDto GetUpdateDtoSample()
    {
        return GetCommentModels.GetUpdateDtoSample();
    }

    protected override Comment GetModelSample()
    {
        return GetCommentModels.GetSample();
    }
}
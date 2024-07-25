using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.WithoutSeoAddition.HistoryQueries;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.SearchHistories;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

namespace WebApiForHikka.Test.Controllers.CrudControllers.WithoutSeoAddition;

public class SearchHistoryControllerTest : CrudControllerBaseTest<
    SearchHistoryController,
    SearchHistoryService,
    SearchHistory,
    ISearchHistoryRepository,
    UpdateSearchHistoryDto,
    CreateSearchHistoryDto,
    GetSearchHistoryDto,
    ReturnPageDto<GetSearchHistoryDto>
>

{
    protected override AllServicesInController GetAllServices(IServiceCollection alternativeServices)
    {
        var dbContext = GetDatabaseContext();

        var repository = new SearchHistoryRepository(dbContext);
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);

        return new AllServicesInController(new SearchHistoryService(repository), userManager, roleManager);
    }

    protected override ICollection<SearchHistory> GetCollectionOfModels(int howMany)
    {
        ICollection<SearchHistory> seoAdditions = new List<SearchHistory>();
        for (var i = 0; i < howMany; ++i) seoAdditions.Add(GetModelSample());
        return seoAdditions;
    }

    protected override async Task<SearchHistoryController> GetController(AllServicesInController allServicesInController,
        IServiceProvider alternativeServices)
    {
        var allServices = allServicesInController;

        return new SearchHistoryController(
            allServices.CrudService,
            _mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager,
                allServicesInController.RoleManager)
        );
    }


    protected override CreateSearchHistoryDto GetCreateDtoSample()
    {
        return GetSearchHistoryModels.GetCreateDtoSample();
    }

    protected override GetSearchHistoryDto GetGetDtoSample()
    {
        return GetSearchHistoryModels.GetGetDtoSample();
    }

    protected override SearchHistory GetModelSample()
    {
        return GetSearchHistoryModels.GetModelSample();
    }

    protected override UpdateSearchHistoryDto GetUpdateDtoSample()
    {
        return GetSearchHistoryModels.GetUpdateDtoSample();
    }
}
// using Microsoft.Extensions.DependencyInjection;
// using WebApiForHikka.Application.SeoAdditions;
// using WebApiForHikka.Application.WithSeoAddition.Characters;
// using WebApiForHikka.Application.WithSeoAddition.Collections;
// using WebApiForHikka.Domain.Models;
// using WebApiForHikka.Domain.Models.WithSeoAddition;
// using WebApiForHikka.Dtos.Dto.WithSeoAddition.Characters;
// using WebApiForHikka.Dtos.Dto.WithSeoAddition.Tags;
// using WebApiForHikka.Dtos.Shared;
// using WebApiForHikka.EfPersistence.Repositories;
// using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
// using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
// using WebApiForHikka.Test.Controllers.Shared;
// using WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition;
//
// namespace WebApiForHikka.Test.Controllers.CrudControllers.WithSeoAddition;
//
// public class CharacterControllerTest : CrudControllerBaseWithSeoAddition<
//     CharacterController,
//     CharacterService,
//     Character,
//     ICharacterRepository,
//     UpdateCharacterDto,
//     CreateCharacterDto,
//     GetCharacterDto,
//     ReturnPageDto<GetCharacterDto>
// >
// {
//     protected override AllServicesInControllerWithSeoAddition GetAllServices(IServiceCollection alternativeServices)
//     {
//         var dbContext = GetDatabaseContext();
//
//         var seoAdditionRepository = new SeoAdditionRepository(dbContext);
//         var characterRepository = new CharacterRepository(dbContext);
//         var userManager = GetUserManager(dbContext);
//         var roleManager = GetRoleManager(dbContext);
//
//         return new AllServicesInControllerWithSeoAddition(new CharacterService(characterRepository),
//             new SeoAdditionService(seoAdditionRepository), userManager, roleManager);
//     }
//
//     protected override async Task<CharacterController> GetController(AllServicesInController allServicesInController,
//         IServiceProvider alternativeServices)
//     {
//         var allServices = allServicesInController as AllServicesInControllerWithSeoAddition ??
//                           throw new Exception("method getController in CharacterControllerTest");
//
//         return new CharacterController(
//             allServices.CrudService,
//             allServices.SeoAdditionService,
//             _mapper,
//             await GetHttpContextAccessForAdminUser(allServicesInController.UserManager,
//                 allServicesInController.RoleManager)
//         );
//     }
//
//     protected override CreateCharacterDto GetCreateDtoSample()
//     {
//         return CharacterModels.GetCreateDtoSample();
//     }
//
//     protected override GetCharacterDto GetGetDtoSample()
//     {
//         return CharacterModels.GetGetDtoSample();
//     }
//
//     protected override Character GetModelSample()
//     {
//         return CharacterModels.GetModelSample();
//     }
//
//     protected override UpdateCharacterDto GetUpdateDtoSample()
//     {
//         return CharacterModels.GetUpdateDtoSample();
//     }
// }
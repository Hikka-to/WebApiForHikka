// using FakeItEasy;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;
// using Moq;
// using WebApiForHikka.Application.Users;
// using WebApiForHikka.Application.WithoutSeoAddition.UserSettings;
// using WebApiForHikka.Constants.Models.Users;
// using WebApiForHikka.Domain.Models;
// using WebApiForHikka.Domain.Models.WithoutSeoAddition;
// using WebApiForHikka.Dtos.Dto.Users;
// using WebApiForHikka.Dtos.ResponseDto;
// using WebApiForHikka.EfPersistence.Data;
// using WebApiForHikka.EfPersistence.Repositories;
// using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
// using WebApiForHikka.SharedFunction.Helpers.LinkFactory;
// using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
// using WebApiForHikka.Test.Controllers.Shared;
// using WebApiForHikka.WebApi.Controllers;
// using WebApiForHikka.WebApi.Helper.FileHelper;
//
// namespace WebApiForHikka.Test.Controllers.Users;
//
// public class UserControllerTest : BaseControllerTest
// {
//     protected IUserService GetUserService(HikkaDbContext dbContext, UserManager<User> userManager)
//     {
//         var userRepository = new UserRepository(dbContext, userManager);
//
//         Mock<IFileHelper> fileHelperMock = new Mock<IFileHelper>();
//
//         fileHelperMock.Setup(m => m.DeleteFile(It.IsAny<string[]>(), It.IsAny<string>()));
//
//         return new UserService(userRepository, fileHelperMock.Object);
//     }
//
//
//
//     [Fact]
//     public async Task Register_ValidModel_ReturnsOk()
//     {
//         // Arrange
//         var userRegistrationDto = GetUserModels.GetCreateSampleDto();
//
//         var dbContext = GetDatabaseContext();
//         var userManager = GetUserManager(dbContext);
//         var userService = GetUserService(dbContext, userManager);
//         var jwtTokenFactory = GetJwtTokenFactory(userManager);
//         var roleManager = GetRoleManager(dbContext);
//         
//
//         var controller = new UserController(userService, jwtTokenFactory, Configuration, roleManager, _mapper,
//             GetHttpContextAccessForAnonymUser());
//
//         // Act
//         var result = await controller.Create(userRegistrationDto, CancellationToken.None);
//
//         // Assert
//         var okResult = Assert.IsType<OkObjectResult>(result);
//         var response = Assert.IsType<RegistratedResponseUserDto>(okResult.Value);
//         Assert.Equal(UserStringConstants.MessageUserRegistrated, response.Message);
//     }
//
//     [Fact]
//     public async Task Get_ValidId_ReturnsOkWithUser()
//     {
//         // Arrange
//         var userId = Guid.NewGuid();
//         var dbContext = GetDatabaseContext();
//         var roleManager = GetRoleManager(dbContext);
//         var userManager = GetUserManager(dbContext);
//         var userService = A.Fake<UserService>();
//         var jwtTokenFactory = GetJwtTokenFactory(userManager);
//         var user = GetUserModels.GetSample();
//
//         A.CallTo(() => userService.GetAsync(userId, A<CancellationToken>.Ignored)).Returns(user);
//
//         var controller = new UserController(userService, jwtTokenFactory, Configuration, roleManager, _mapper,
//             await GetHttpContextAccessForAdminUser(userManager, roleManager));
//
//         // Act
//         var result = await controller.Get(userId, CancellationToken.None);
//
//         // Assert
//         var okResult = Assert.IsType<OkObjectResult>(result);
//     }
//
//     [Fact]
//     public async Task Put_ValidModel_ReturnsNoContent()
//     {
//         // Arrange
//         var updateUserDto = GetUserModels.GetUpdateDtoSample();
//
//         var dbContext = GetDatabaseContext();
//         var userManager = GetUserManager(dbContext);
//         var roleManager = GetRoleManager(dbContext);
//         var userService = A.Fake<UserService>();
//         var jwtTokenFactory = GetJwtTokenFactory(userManager);
//         var role = await roleManager.FindByNameAsync(UserStringConstants.AdminRole);
//         var user = GetUserModels.GetSample();
//         A.CallTo(() => userService.GetAsync(updateUserDto.Id, A<CancellationToken>.Ignored)).Returns(user);
//
//         var controller = new UserController(userService, jwtTokenFactory, Configuration, roleManager, _mapper,
//             await GetHttpContextAccessForAdminUser(userManager, roleManager));
//
//         // Act
//         var result = await controller.Put(updateUserDto, CancellationToken.None);
//
//         // Assert
//         Assert.IsType<NoContentResult>(result);
//     }
//
//     [Fact]
//     public async Task Delete_ValidId_ReturnsNoContent()
//     {
//         // Arrange
//         var dbContext = GetDatabaseContext();
//         var userManager = GetUserManager(dbContext);
//         var roleManager = GetRoleManager(dbContext);
//         var userService = GetUserService(dbContext, userManager);
//         var jwtTokenFactory = GetJwtTokenFactory(userManager);
//         var controller = new UserController(userService, jwtTokenFactory, Configuration, roleManager, _mapper,
//             await GetHttpContextAccessForAdminUser(userManager, roleManager));
//
//         var role = await roleManager.FindByNameAsync(UserStringConstants.AdminRole);
//
//         var user = GetUserModels.GetSampleForUpdate();
//
//         // var user = GetUserModels.GetSample();
//         // user.UserName = "test123";
//         // user.Email = "skfgk@gmail.com";
//
//         var userId = await userService.CreateAsync(user, CancellationToken);
//
//         // Act
//         var result = await controller.Delete(userId, CancellationToken);
//
//         // Assert
//         Assert.IsType<NoContentResult>(result);
//     }
// }


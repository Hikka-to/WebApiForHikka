using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.SeoAdditions;
using WebApiForHikka.Dtos.ResponseDto;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.Test.Controller.Shared;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.Test.Controllers.Shared;

public abstract class CrudControllerBaseWithSeoAddition<TController, TCrudService,
    TModel, TIRepository,
    TUpdateDto, TCreateDto, TGetDto, TReturnPageDto>
    : CrudControllerBaseTest<
        TController, TCrudService, TModel, TIRepository, TUpdateDto, TCreateDto, TGetDto, TReturnPageDto
    >
    where TController : ICrudController<TUpdateDto, TCreateDto>
    where TCrudService : CrudService<TModel, TIRepository>
    where TModel : ModelWithSeoAddition
    where TIRepository : ICrudRepository<TModel>
    where TUpdateDto : UpdateDtoWithSeoAddition
    where TReturnPageDto : ReturnPageDto<TGetDto>
{

    protected record AllServicesInControllerWithSeoAddition(TCrudService crudService, ISeoAdditionService seoAdditionService, UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager) : AllServicesInController(crudService, userManager, roleManager)
    {
        public ISeoAdditionService SeoAdditionService = seoAdditionService;
    }

    protected CreateSeoAdditionDto GetSeoAdditionCreateDtoSample()
    {
        return new CreateSeoAdditionDto()
        {
            Description = Faker.Lorem.GetFirstWord(),
            Slug = Faker.Lorem.GetFirstWord(),
            Title = Faker.Lorem.GetFirstWord(),
            Image = Faker.Lorem.GetFirstWord(),
            ImageAlt = Faker.Lorem.GetFirstWord(),
            SocialImage = Faker.Lorem.GetFirstWord(),
            SocialImageAlt = Faker.Lorem.GetFirstWord(),
            SocialTitle = Faker.Lorem.GetFirstWord(),
            SocialType = Faker.Lorem.GetFirstWord(),
        };
    }

    protected SeoAddition GetSeoAdditionSample()
    {
        return new SeoAddition()
        {
            Description = Faker.Lorem.GetFirstWord(),
            Slug = Faker.Lorem.GetFirstWord(),
            Title = Faker.Lorem.GetFirstWord(),
            Image = Faker.Lorem.GetFirstWord(),
            ImageAlt = Faker.Lorem.GetFirstWord(),
            SocialImage = Faker.Lorem.GetFirstWord(),
            SocialImageAlt = Faker.Lorem.GetFirstWord(),
            SocialTitle = Faker.Lorem.GetFirstWord(),
            SocialType = Faker.Lorem.GetFirstWord(),
            Id = new Guid(),
        };
    }

    protected UpdateSeoAdditionDto GetSeoAddtionUpdateDtoSample()
    {
        return new UpdateSeoAdditionDto()
        {
            Description = Faker.Lorem.GetFirstWord(),
            Slug = Faker.Lorem.GetFirstWord(),
            Title = Faker.Lorem.GetFirstWord(),
            Image = Faker.Lorem.GetFirstWord(),
            ImageAlt = Faker.Lorem.GetFirstWord(),
            SocialImage = Faker.Lorem.GetFirstWord(),
            SocialImageAlt = Faker.Lorem.GetFirstWord(),
            SocialTitle = Faker.Lorem.GetFirstWord(),
            SocialType = Faker.Lorem.GetFirstWord(),
            Id = new Guid(),
        };
    }

    protected GetSeoAdditionDto GetSeoAdditionGetDtoSample()
    {
        return new GetSeoAdditionDto()
        {
            Description = Faker.Lorem.GetFirstWord(),
            Slug = Faker.Lorem.GetFirstWord(),
            Title = Faker.Lorem.GetFirstWord(),
            Image = Faker.Lorem.GetFirstWord(),
            ImageAlt = Faker.Lorem.GetFirstWord(),
            SocialImage = Faker.Lorem.GetFirstWord(),
            SocialImageAlt = Faker.Lorem.GetFirstWord(),
            SocialTitle = Faker.Lorem.GetFirstWord(),
            SocialType = Faker.Lorem.GetFirstWord(),
            Id = new Guid(),
        };
    }

    [Fact]
    public override async Task CrudController_Put_ReturnsNoContent()
    {
        //Arrange
        var serviceCollection = new ServiceCollection();
        var services = GetAllServices(serviceCollection);
        var serviceProvider = serviceCollection.BuildServiceProvider();
        TController controller = await GetController(services, serviceProvider);


        //Act

        var createDto = GetCreateDtoSample();
        MutationBeforeDtoCreation(createDto, services, serviceProvider);
        CreateResponseDto create = (await controller.Create(createDto, CancellationToken) as OkObjectResult).Value as CreateResponseDto;

        TModel model = await services.CrudService.GetAsync(create.Id, CancellationToken);

        var updateDto = GetUpdateDtoSample();
        updateDto.Id = model.Id;
        updateDto.SeoAddition.Id = model.SeoAddition.Id;
        MutationBeforeDtoUpdate(updateDto, services, serviceProvider);
        var result = await controller.Put(updateDto, CancellationToken);


        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NoContentResult>();


    }

}

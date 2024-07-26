using Faker;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Enums;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.SeoAdditions;
using WebApiForHikka.Dtos.ResponseDto;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.WebApi.Shared;
using Enum = Faker.Enum;

namespace WebApiForHikka.Test.Controllers.Shared;

public abstract class CrudControllerBaseWithSeoAddition<TController, TCrudService,
    TModel, TIRepository,
    TUpdateDto, TCreateDto, TGetDto, TReturnPageDto>
    : CrudControllerBaseTest<
        TController, TCrudService, TModel, TIRepository, TUpdateDto, TCreateDto, TGetDto, TReturnPageDto
    >
    where TController : ICrudController<TUpdateDto, TCreateDto>
    where TCrudService : CrudService<TModel, TIRepository>
    where TModel : class, IModelWithSeoAddition
    where TIRepository : ICrudRepository<TModel>
    where TUpdateDto : UpdateDtoWithSeoAddition
    where TReturnPageDto : ReturnPageDto<TGetDto>
{
    protected CreateSeoAdditionDto GetSeoAdditionCreateDtoSample()
    {
        return new CreateSeoAdditionDto
        {
            Description = Lorem.GetFirstWord(),
            Slug = Lorem.GetFirstWord(),
            Title = Lorem.GetFirstWord(),
            Image = Lorem.GetFirstWord(),
            ImageAlt = Lorem.GetFirstWord(),
            SocialImage = Lorem.GetFirstWord(),
            SocialImageAlt = Lorem.GetFirstWord(),
            SocialTitle = Lorem.GetFirstWord(),
            SocialType = Enum.Random<SocialType>()
        };
    }

    protected SeoAddition GetSeoAdditionSample()
    {
        return new SeoAddition
        {
            Description = Lorem.GetFirstWord(),
            Slug = Lorem.GetFirstWord(),
            Title = Lorem.GetFirstWord(),
            Image = Lorem.GetFirstWord(),
            ImageAlt = Lorem.GetFirstWord(),
            SocialImage = Lorem.GetFirstWord(),
            SocialImageAlt = Lorem.GetFirstWord(),
            SocialTitle = Lorem.GetFirstWord(),
            SocialType = Enum.Random<SocialType>(),
            Id = new Guid()
        };
    }

    protected UpdateSeoAdditionDto GetSeoAddtionUpdateDtoSample()
    {
        return new UpdateSeoAdditionDto
        {
            Description = Lorem.GetFirstWord(),
            Slug = Lorem.GetFirstWord(),
            Title = Lorem.GetFirstWord(),
            Image = Lorem.GetFirstWord(),
            ImageAlt = Lorem.GetFirstWord(),
            SocialImage = Lorem.GetFirstWord(),
            SocialImageAlt = Lorem.GetFirstWord(),
            SocialTitle = Lorem.GetFirstWord(),
            SocialType = Enum.Random<SocialType>(),
            Id = new Guid()
        };
    }

    protected GetSeoAdditionDto GetSeoAdditionGetDtoSample()
    {
        return new GetSeoAdditionDto
        {
            Description = Lorem.GetFirstWord(),
            Slug = Lorem.GetFirstWord(),
            Title = Lorem.GetFirstWord(),
            Image = Lorem.GetFirstWord(),
            ImageAlt = Lorem.GetFirstWord(),
            SocialImage = Lorem.GetFirstWord(),
            SocialImageAlt = Lorem.GetFirstWord(),
            SocialTitle = Lorem.GetFirstWord(),
            SocialType = Enum.Random<SocialType>(),
            Id = new Guid()
        };
    }

    [Fact]
    public override async Task CrudController_Put_ReturnsNoContent()
    {
        //Arrange
        var serviceCollection = new ServiceCollection();
        var services = GetAllServices(serviceCollection);
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var controller = await GetController(services, serviceProvider);


        //Act

        var createDto = GetCreateDtoSample();
        MutationBeforeDtoCreation(createDto, services, serviceProvider);
        var create =
            (await controller.Create(createDto, CancellationToken) as OkObjectResult)!.Value as CreateResponseDto;

        var model = await services.CrudService.GetAsync(create!.Id, CancellationToken);

        var updateDto = GetUpdateDtoSample();
        updateDto.Id = model!.Id;
        updateDto.SeoAddition.Id = model.SeoAddition.Id;
        MutationBeforeDtoUpdate(updateDto, services, serviceProvider);
        var result = await controller.Put(updateDto, CancellationToken);


        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NoContentResult>();
    }

    protected record AllServicesInControllerWithSeoAddition(
        TCrudService CrudService,
        ISeoAdditionService SeoAdditionService,
        UserManager<User> UserManager,
        RoleManager<IdentityRole<Guid>> RoleManager) : AllServicesInController(CrudService, UserManager, RoleManager)
    {
        public ISeoAdditionService SeoAdditionService = SeoAdditionService;
    }
}
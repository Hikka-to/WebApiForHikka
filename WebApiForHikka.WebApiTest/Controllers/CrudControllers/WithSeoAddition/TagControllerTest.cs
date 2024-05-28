using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.WithSeoAddition.Tags;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Tags;
using WebApiForHikka.Dtos.ResponseDto;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
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
    protected override AllServicesInControllerWithSeoAddition GetAllServices()
    {
        var dbContext = GetDatabaseContext();

        var seoAdditionRepository = new SeoAdditionRepository(dbContext);
        var tagRepository = new TagRepository(dbContext);

        return new AllServicesInControllerWithSeoAddition(new TagService(tagRepository), new SeoAdditionService(seoAdditionRepository));
    }



    protected override TagController GetController(AllServicesInController allServicesInController)
    {
        AllServicesInControllerWithSeoAddition allServices = allServicesInController as AllServicesInControllerWithSeoAddition ?? throw new Exception("method getController in TagControllerTest");

        return new TagController(
            allServices.CrudService,
            allServices.SeoAdditionService,
            _mapper,
            GetHttpContextAccessForAdminUser()
            );
    }


    protected override CreateTagDto GetCreateDtoSample()
    {
        return new CreateTagDto()
        {
            Alises = Faker.Lorem.Words(2).ToList(),
            EngName = Faker.Lorem.GetFirstWord(),
            IsGenre = Faker.Boolean.Random(),
            Name = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionCreateDtoSample(),
        };
    }

    protected override GetTagDto GetGetDtoSample()
    {
        return new GetTagDto()
        {
            Alises = Faker.Lorem.Words(2).ToList(),
            EngName = Faker.Lorem.GetFirstWord(),
            Name = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionGetDtoSample(),
            IsGenre = Faker.Boolean.Random(),
            Id = new Guid(),
        };
    }

    protected override Tag GetModelSample()
    {
        return new Tag()
        {
            Alises = Faker.Lorem.Words(2).ToList(),
            EngName = Faker.Lorem.GetFirstWord(),
            IsGenre = Faker.Boolean.Random(),
            Name = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionSample(),
            Id = new Guid(),
        };
    }

    protected override UpdateTagDto GetUpdateDtoSample()
    {
        return new UpdateTagDto()
        {
            Alises = Faker.Lorem.Words(2).ToList(),
            EngName = Faker.Lorem.GetFirstWord(),
            IsGenre = Faker.Boolean.Random(),
            Name = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAddtionUpdateDtoSample(),
            Id = new Guid(),
        };
    }

   }
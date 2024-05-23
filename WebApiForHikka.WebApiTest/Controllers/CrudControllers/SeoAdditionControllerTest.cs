﻿using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.SeoAddition;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Controller.Shared;
using WebApiForHikka.WebApi.Controllers;

namespace WebApiForHikka.Test.Controllers.CrudControllers;

public class SeoAdditionControllerTest : CrudControllerBaseTest<
    SeoAdditionController,
    SeoAdditionService,
    SeoAddition,
    ISeoAdditionRepository,
    UpdateSeoAdditionDto,
    CreateSeoAdditionDto,
    GetSeoAdditionDto,
    ReturnPageDto<GetSeoAdditionDto>
    >
{
    protected override SeoAdditionController GetController(SeoAdditionService crudService)
    {

        return new SeoAdditionController(
            crudService,
            _mapper,
            GetHttpContextAccessForAdminUser()
            );

    }

    protected override CreateSeoAdditionDto GetCreateDtoSample()
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

    protected override SeoAddition GetModelSample()
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

    protected override UpdateSeoAdditionDto GetUpdateDtoSample()
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

    protected override GetSeoAdditionDto GetGetDtoSample()
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

    protected override ICollection<SeoAddition> GetCollectionOfModels(int howMany)
    {
        ICollection<SeoAddition> seoAdditions = new List<SeoAddition>();
        for (int i = 0; i < howMany; ++i)
        {
            seoAdditions.Add(GetModelSample());
        }
        return seoAdditions;
    }


    protected override SeoAdditionService GetCrudService()
    {
        var res = new SeoAdditionRepository(GetDatabaseContext());

        return new SeoAdditionService(res);
    }

   }

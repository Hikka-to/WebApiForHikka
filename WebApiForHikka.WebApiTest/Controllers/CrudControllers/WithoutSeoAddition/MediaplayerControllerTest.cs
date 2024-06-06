﻿using WebApiForHikka.Application.WithSeoAddition.Mediaplayers;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Mediaplayers;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.Test.Controller.Shared;
using WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

namespace WebApiForHikka.Test.Controllers.CrudControllers.WithoutSeoAddition;

public class MediaplayerControllerTest : CrudControllerBaseTest<
    MediaplayerController,
    MediaplayerService,
    Mediaplayer,
    IMediaplayerRepository,
    UpdateMediaplayerDto,
    CreateMediaplayerDto,
    GetMediaplayerDto,
    ReturnPageDto<GetMediaplayerDto>
    >

{
    protected override AllServicesInController GetAllServices()
    {
        var dbContext = GetDatabaseContext();

         var repository = new MediaplayerRepository(dbContext);

        return new AllServicesInController( new MediaplayerService(repository));
    }

    protected override ICollection<Mediaplayer> GetCollectionOfModels(int howMany)
    {
        ICollection<Mediaplayer> seoAdditions = new List<Mediaplayer>();
        for (int i = 0; i < howMany; ++i)
        {
            seoAdditions.Add(GetModelSample());
        }
        return seoAdditions;

    }

    protected override MediaplayerController GetController(AllServicesInController allServicesInController)
    {
        AllServicesInController allServices = allServicesInController;

        return new MediaplayerController(
            allServices.CrudService,
            _mapper,
            GetHttpContextAccessForAdminUser()
            );
    }

   
    protected override CreateMediaplayerDto GetCreateDtoSample()
    {
        return new CreateMediaplayerDto()
        {
            Name = Faker.Lorem.GetFirstWord(),
            Icon = Faker.Lorem.GetFirstWord(),
        };
    }

    protected override GetMediaplayerDto GetGetDtoSample()
    {
        return new GetMediaplayerDto()
        {
            Name = Faker.Lorem.GetFirstWord(),
            Icon = Faker.Lorem.GetFirstWord(),
            Id = new Guid(),
        };
    }

    protected override Mediaplayer GetModelSample()
    {
        return new Mediaplayer()
        {
            Name = Faker.Lorem.GetFirstWord(),
            Icon = Faker.Lorem.GetFirstWord(),
            Id = new Guid(),
        };
    }

    protected override UpdateMediaplayerDto GetUpdateDtoSample()
    {
        return new UpdateMediaplayerDto()
        {
            Name = Faker.Lorem.GetFirstWord(),
            Icon = Faker.Lorem.GetFirstWord(),
            Id = new Guid(),
        };
    }
}

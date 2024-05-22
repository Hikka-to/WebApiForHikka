using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.SeoAddition;
using WebApiForHikka.Dtos.Shared;
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
    protected override SeoAdditionController GetController()
    {

        return new SeoAdditionController(
            _crudService,
            _mapper,
            GetHttpContextAccessForAdminUser()
            );

    }

    protected override CreateSeoAdditionDto GetCreateDtoSample()
    {
        return new CreateSeoAdditionDto()
        {
            Description = "test",
            Slug = "test",
            Title = "test",
            Image = "test",
            ImageAlt = "test",
            SocialImage = "test",
            SocialImageAlt = "test",
            SocialTitle = "test",
            SocialType = "test",
        };
    }

    protected override SeoAddition GetModelSample()
    {
        return new SeoAddition()
        {
            Description = "test",
            Slug = "test",
            Title = "test",
            Image = "test",
            ImageAlt = "test",
            SocialImage = "test",
            SocialImageAlt = "test",
            SocialTitle = "test",
            SocialType = "test",
            Id = new Guid(),
        };
    }

    protected override UpdateSeoAdditionDto GetUpdateDtoSample()
    {
        return new UpdateSeoAdditionDto()
        {
            Description = "test",
            Slug = "test",
            Title = "test",
            Image = "test",
            ImageAlt = "test",
            SocialImage = "test",
            SocialImageAlt = "test",
            SocialTitle = "test",
            SocialType = "test",
            Id = new Guid(),
        };
    }

    protected override GetSeoAdditionDto GetGetDtoSample()
    {
        return new GetSeoAdditionDto()
        {
            Description = "test",
            Slug = "test",
            Title = "test",
            Image = "test",
            ImageAlt = "test",
            SocialImage = "test",
            SocialImageAlt = "test",
            SocialTitle = "test",
            SocialType = "test",
            Id = new Guid(),
        };
    }
}

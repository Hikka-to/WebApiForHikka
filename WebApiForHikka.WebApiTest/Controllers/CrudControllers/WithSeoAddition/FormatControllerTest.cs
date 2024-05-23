using WebApiForHikka.Application.Formats;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.Formats;
using WebApiForHikka.Dtos.Dto.SeoAddition;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.Test.Controller.Shared;
using WebApiForHikka.WebApi.Controllers;
using WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition;

namespace WebApiForHikka.Test.Controllers.CrudControllers.WithSeoAddition;

//public class FormatControllerTest : CrudControllerBaseTest<
//    FormatController,
//    FormatService,
//    Format,
//    IFormatRepository,
//    UpdateFormatDto,
//    CreateFormatDto,
//    GetFormatDto,
//    ReturnPageDto<GetFormatDto>
//    >
//{
//    protected override ICollection<Format> GetCollectionOfModels(int howMany)
//    {
//        throw new NotImplementedException();
//    }

//    protected override FormatController GetController(FormatService crudService)
//    {
//        return new FormatController(
//           crudService,
//           _mapper,
//           GetHttpContextAccessForAdminUser()
//       );
//    }

//    protected override CreateFormatDto GetCreateDtoSample()
//    {
//        throw new NotImplementedException();
//    }

//    protected override FormatService GetCrudService()
//    {
//        throw new NotImplementedException();
//    }

//    protected override GetFormatDto GetGetDtoSample()
//    {
//        throw new NotImplementedException();
//    }

//    protected override Format GetModelSample()
//    {
//        throw new NotImplementedException();
//    }

//    protected override UpdateFormatDto GetUpdateDtoSample()
//    {
//        throw new NotImplementedException();
//    }
//}

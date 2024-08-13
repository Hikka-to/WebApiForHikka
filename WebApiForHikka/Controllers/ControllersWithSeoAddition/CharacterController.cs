// using AutoMapper;
// using Microsoft.AspNetCore.Mvc;
// using WebApiForHikka.Application.SeoAdditions;
// using WebApiForHikka.Application.WithSeoAddition.Characters;
// using WebApiForHikka.Domain.Models;
// using WebApiForHikka.Domain.Models.WithSeoAddition;
// using WebApiForHikka.Dtos.Dto.WithSeoAddition.Characters;
// using WebApiForHikka.Dtos.ResponseDto;
// using WebApiForHikka.Dtos.Shared;
// using WebApiForHikka.WebApi.Shared;
//
// namespace WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition;
//
// public class CharacterController : CrudControllerForModelWithSeoAddition<GetCharacterDto,
//     UpdateCharacterDto,
//     CreateCharacterDto,
//     ICharacterService,
//     Character
// >
// {
//     public CharacterController(
//         ICharacterService crudService,
//         ISeoAdditionService seoAdditionService,
//         IMapper mapper,
//         IHttpContextAccessor httpContextAccessor)
//         : base(crudService, seoAdditionService, mapper, httpContextAccessor)
//     {
//     }
//
//     [HttpPost("Create")]
//     public override async Task<IActionResult> Create([FromBody] CreateCharacterDto dto, CancellationToken cancellationToken)
//     {
//         var errorEndPoint = ValidateRequest(new ThingsToValidateBase());
//         if (errorEndPoint.IsError) return errorEndPoint.GetError();
//
//         var model = _mapper.Map<Character>(dto);
//
//         var seoAddition = _mapper.Map<SeoAddition>(dto.SeoAddition);
//         await _seoAdditionService.CreateAsync(seoAddition, cancellationToken);
//
//         model.SeoAddition = seoAddition;
//
//         var createdId = await CrudRelationService.CreateAsync(model, cancellationToken);
//
//         return Ok(new CreateResponseDto { Id = createdId });
//     }
//
//     [HttpPut("Update")]
//     public override async Task<IActionResult> Put([FromBody] UpdateCharacterDto dto, CancellationToken cancellationToken)
//     {
//         var errorEndPoint = ValidateRequestForUpdateWithSeoAddtionEndPoint(new ThingsToValidateWithSeoAdditionForUpdate
//         {
//             UpdateDto = dto,
//             IdForSeoAddition = dto.SeoAddition.Id
//         });
//         if (errorEndPoint.IsError) return errorEndPoint.GetError();
//
//         var model = _mapper.Map<Character>(dto);
//         var seoAdditionModel = _mapper.Map<SeoAddition>(dto.SeoAddition);
//         await _seoAdditionService.UpdateAsync(seoAdditionModel, cancellationToken);
//
//         model.SeoAddition = (await _seoAdditionService.GetAsync(seoAdditionModel.Id, cancellationToken))!;
//
//         await CrudRelationService.UpdateAsync(model, cancellationToken);
//
//         return NoContent();
//     }
// }
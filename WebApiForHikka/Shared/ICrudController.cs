using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Dtos.Dto.SharedDtos;

namespace WebApiForHikka.WebApi.Shared;

public interface ICrudController<TUpdateDto, TCreateDto>
{
    public Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken);
    public Task<IActionResult> Create([FromBody] TCreateDto dto, CancellationToken cancellationToken);

    public Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken);

    public Task<IActionResult> Put([FromBody] TUpdateDto dto, CancellationToken cancellationToken);

    public Task<IActionResult> GetAll([FromQuery] FilterPaginationDto paginationDto,
        CancellationToken cancellationToken);

}
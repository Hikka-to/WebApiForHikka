using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Domain;

namespace WebApiForHikka.WebApi.Shared;
public interface ICrudController<UpdateDto, CreateDto>
{
    public Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken);

    public Task<IActionResult> Put([FromBody] UpdateDto dto, CancellationToken cancellationToken);

    public Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken);

    public Task<IActionResult> GetAll([FromQuery] FilterPaginationDto paginationDto, CancellationToken cancellationToken);

    public Task<IActionResult> Create([FromBody] CreateDto dto, CancellationToken cancellationToken);
}
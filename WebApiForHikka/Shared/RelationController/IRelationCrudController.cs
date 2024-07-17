using Microsoft.AspNetCore.Mvc;

namespace WebApiForHikka.WebApi.Shared;

public interface IRelationCrudController
{
    public Task<IActionResult> Create([FromRoute] Guid firstId, [FromRoute] Guid secodId,
        CancellationToken cancellationToken);

    public Task<IActionResult> Delete([FromRoute] Guid firstId, [FromRoute] Guid secodId,
        CancellationToken cancellationToken);
}
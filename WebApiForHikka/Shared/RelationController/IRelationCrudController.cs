using Microsoft.AspNetCore.Mvc;

namespace WebApiForHikka.WebApi.Shared.RelationController;

public interface IRelationCrudController
{
    public Task<IActionResult> Create([FromRoute] Guid firstId, [FromRoute] Guid secondId,
        CancellationToken cancellationToken);

    public Task<IActionResult> Delete([FromRoute] Guid firstId, [FromRoute] Guid secondId,
        CancellationToken cancellationToken);
}
using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.WithSeoAddition.Tags;

public class TagService : CrudService<Tag, ITagRepository>, ITagService
{
    public TagService(ITagRepository repository) : base(repository)
    {
    }
}

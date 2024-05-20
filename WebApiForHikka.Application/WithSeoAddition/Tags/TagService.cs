using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.WithSeoAddition.Tags;

public class TagService(ITagRepository repository) : CrudService<Tag, ITagRepository>(repository), ITagService;
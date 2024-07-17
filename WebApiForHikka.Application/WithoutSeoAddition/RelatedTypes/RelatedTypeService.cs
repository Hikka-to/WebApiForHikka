using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Application.WithoutSeoAddition.RelatedTypes;

public class RelatedTypeService(IRelatedTypeRepository repository)
    : CrudService<RelatedType, IRelatedTypeRepository>(repository), IRelatedTypeService;
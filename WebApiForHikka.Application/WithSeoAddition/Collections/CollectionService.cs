using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.WithSeoAddition.Collections;

public class CollectionService(ICollectionRepository collectionRepository)
    : CrudService<Collection, ICollectionRepository>(collectionRepository), ICollectionService;
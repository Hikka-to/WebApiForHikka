using WebApiForHikka.Application.WithSeoAddition.Dubs;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Shared.Attributes;

namespace WebApiForHikka.Dtos.MyOwnValidationAttribute.EntityValidationAttributes.ListEntityValidation;

public class DubListValidationAttribute : ListEntityValidationAttribute<IDubService, Dub>;

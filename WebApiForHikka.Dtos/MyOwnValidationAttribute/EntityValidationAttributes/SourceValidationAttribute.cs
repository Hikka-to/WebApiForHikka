using WebApiForHikka.Application.Sources;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Shared.Attributes;

namespace WebApiForHikka.Dtos.MyOwnValidationAttribute.EntityValidationAttributes;

public class SourceValidationAttribute : EntityValidationAttribute<Source, ISourceService>;
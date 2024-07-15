using WebApiForHikka.Application.WithSeoAddition.Countries;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Shared.Attributes;

namespace WebApiForHikka.Dtos.MyOwnValidationAttribute.EntityValidationAttributes.ListEntityValidation;

public class CountryListValidationAtrribute : ListEntityValidationAttribute<ICountryService, Country>;
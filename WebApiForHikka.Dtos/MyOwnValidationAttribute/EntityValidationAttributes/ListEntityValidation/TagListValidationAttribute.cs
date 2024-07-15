using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Application.WithSeoAddition.Tags;
using WebApiForHikka.Constants.Controllers;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Shared.Attributes;

namespace WebApiForHikka.Dtos.MyOwnValidationAttribute.EntityValidationAttributes.ListEntityValidation;


public class TagListValidationAttribute : ListEntityValidationAttribute<ITagService, Tag>;

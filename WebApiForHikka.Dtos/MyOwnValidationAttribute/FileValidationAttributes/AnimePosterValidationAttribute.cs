using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using WebApiForHikka.Constants.Controllers;
using WebApiForHikka.Constants.Shared;

namespace WebApiForHikka.Dtos.MyOwnValidationAttribute.FileValidationAttributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class AnimePosterValidationAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value == null)
        {
            ErrorMessage = ControllerStringConstants.FileMustNotBeNullErrorMessage;
            return false;
        }

        var formFile = value as IFormFile;

        var extention = Path.GetExtension(formFile.FileName);

        if (!SharedStringConstants.AllowedExtensionsList.Contains(extention))
        {
            ErrorMessage = ControllerStringConstants.ThisFileFormatIsntAllowedYouCanUseTheFollowing +
                           string.Join(", ", SharedStringConstants.AllowedExtensionsList);
            return false;
        }

        if (formFile.Length > SharedNumberConstatnts.MaxFileSize)
        {
            ErrorMessage = ControllerStringConstants.MaximumSizeCanBe + SharedNumberConstatnts.MaxFileSize + " bytes";
            return false;
        }

        return true;
    }


    public override string FormatErrorMessage(string name)
    {
        return ErrorMessage!;
    }
}
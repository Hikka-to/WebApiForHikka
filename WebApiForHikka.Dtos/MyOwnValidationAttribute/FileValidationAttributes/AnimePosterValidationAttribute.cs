using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Controllers;
using WebApiForHikka.Constants.Shared;

namespace WebApiForHikka.Dtos.MyOwnValidationAttribute.FileValidationAttributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class AnimePosterValidationAttribute : ValidationAttribute
{


    public override bool IsValid(object? value)
    {
        if (value == null)
        {
            ErrorMessage = ControllerStringConstants.FileMustNotBeNullErrorMessage;
            return false;
        }

        IFormFile formFile = value as IFormFile;

        string extention = Path.GetExtension(formFile.FileName);

        if (!SharedStringConstants.AllowedExtensionsList.Contains(extention))
        {
            ErrorMessage = ControllerStringConstants.ThisFileFormatIsntAllowedYouCanUseTheFollowing + String.Join(", ", SharedStringConstants.AllowedExtensionsList);
            return false;
        }

        if (formFile.Length > SharedNumberConstatnts.MaxFileSize)
        {
            ErrorMessage = ControllerStringConstants.MaximumSizeCanBe + SharedNumberConstatnts.MaxFileSize + " bytes";
            return false;
        }

        return true;
    }



    public override string FormatErrorMessage(string name) =>
       ErrorMessage!;

}

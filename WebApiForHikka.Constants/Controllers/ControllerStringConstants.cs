namespace WebApiForHikka.Constants.Controllers;

public static class ControllerStringConstants
{
    //Policies

    public const string CanAccessEveryone = "CanAccessEveryone";

    public const string CanAccessUserAndAdmin = "CanAccessUserAndAdmin";

    public const string CanAccessOnlyAdmin = "CanAccessOnlyAdmin";



    //Error messages
    public const string ErrorMessageThisEndpointCanAccess = "This endpoint can access only users with the next rolls: ";

    public const string EmailIsntFormatedCorrectlyErrorMessage = "Email isn't formated correctly";

    public const string IdIsRequiredErrorMessage = "Id is required";

    public const string SomethingWentWrongDuringCreateing = "Something went wrong during createing ";

    public const string ModelUpdatedSuccessfully = "Model was udpated successfully";

    public const string SomethingWentWrongDuringUpdateing = "Something went wrong during updateing";

    public const string ThereIsNoSeoAdditionWithThisId = "There isn't seoAddition with this id";

    public const string ModelWithThisIdDoesntExistForUpdateEndPoint = "Model with this id doesn't exit this endpoint only for update models it can't handler ids that don't exist";

    public const string SeoAdditionDoesntConnectToTheModel = "SeoAddtion with this id wasn't attached to the model please don't chage the id in seoAddition";
}
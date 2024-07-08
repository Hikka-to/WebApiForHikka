namespace WebApiForHikka.Constants.Shared;

public static class SharedStringConstants
{
    //General
    public const string Asc = "ascending";

    public const string Desc = "descending";

    //Model
    public const string IdName = "Id";

    //file extensions

    public static readonly IReadOnlyCollection<string> AllowedExtensionsList =
    [
        ".png",
        ".jpg",
        ".web"
    ];
}
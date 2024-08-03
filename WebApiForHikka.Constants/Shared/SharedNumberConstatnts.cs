namespace WebApiForHikka.Constants.Shared;

public static class SharedNumberConstatnts
{
    public const int DefaultItemsInOnePage = 50;
    public const int DefaultPageToStartWith = 1;

    public const int MinPageSize = 1;
    public const int MaxPageSize = 50;

    public const int MinPageNumber = 1;

    public const int HowManyDayExpiresForJwt = 7;

    public const int NameLength = 64;

    public const int SlugLength = 128;

    public const int UrlLength = 2048;

    public const long MaxFileSize = 10 << 20; // 10 MB
}
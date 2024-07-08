using WebApiForHikka.Constants.Shared;

namespace WebApiForHikka.Constants.Models.Animes;

public class AnimeNumberConstants
{
    public const int NameLength = 156;

    public const int ImageNameLength = SharedNumberConstatnts.UrlLength;

    public const int RomajiNameLength = 248;

    public const int NativeNameLength = 156;

    public const int PosterPathLength = SharedNumberConstatnts.UrlLength;

    public const float MaxScore = 10.0f;

    public const float LowestScore = 0.0f;
}
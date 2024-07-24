using Microsoft.AspNetCore.Http;

namespace WebApiForHikka.SharedFunction.Helpers.LinkFactory;

public class LinkFactory : ILinkFactory
{
    public string GetLinkForDowloadImage(HttpRequest httpRequest, string dowloadImageEndpoint,
        string currectEnpointName, string imagePath)
    {
        var scheme = httpRequest.Scheme;

        var hostValue = httpRequest.Host.Value;

        var leftPartOfLink = httpRequest.Path.Value.Substring(0, httpRequest.Path.Value.IndexOf(currectEnpointName));

        var rightPartOfLink = imagePath.Split('\\').Last();

        return $"{scheme}://{hostValue}" +
               leftPartOfLink + $"{dowloadImageEndpoint}/" + rightPartOfLink;
    }
}
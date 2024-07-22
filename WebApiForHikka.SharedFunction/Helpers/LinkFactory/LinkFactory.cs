using Microsoft.AspNetCore.Http;

namespace WebApiForHikka.SharedFunction.Helpers.LinkFactory;

public class LinkFactory : ILinkFactory
{
    public string GetLinkForDowloadImage(HttpRequest httpRequest, string dowloadImageEndpoint, string currectEnpointName, string imagePath)
    {

        string scheme = httpRequest.Scheme;

        string hostValue = httpRequest.Host.Value;

        string leftPartOfLink = httpRequest.Path.Value.Substring(0, httpRequest.Path.Value.IndexOf(currectEnpointName));

        string rightPartOfLink = imagePath.Split('\\').Last();

        return $"{scheme}://{hostValue}" +
                              leftPartOfLink + $"{dowloadImageEndpoint}/" + rightPartOfLink;
    }
}

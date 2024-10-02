using Microsoft.AspNetCore.Http;

namespace WebApiForHikka.SharedFunction.Helpers.LinkFactory;

public class LinkFactory : ILinkFactory
{
    public string GetLinkForDownloadImage(HttpRequest httpRequest, string downloadImageEndpoint,
        string currentEndpointName, string imagePath)
    {
        var scheme = httpRequest.Scheme;

        var hostValue = httpRequest.Host.Value;

        var leftPartOfLink =
            httpRequest.Path.Value?[
                ..httpRequest.Path.Value.IndexOf(currentEndpointName, StringComparison.Ordinal)];

        var rightPartOfLink = imagePath.Split('\\').Last();

        return $"{scheme}://{hostValue}" +
               leftPartOfLink + $"{downloadImageEndpoint}/" + rightPartOfLink;
    }
}
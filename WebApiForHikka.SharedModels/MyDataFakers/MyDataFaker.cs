using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace WebApiForHikka.SharedModels.MyDataFakers;

public static class MyDataFaker
{
    public static string GetFakeUrl => "https://letsenhance.io/static/8f5e523ee6b2479e26ecc91b9c25261e/1015f/MainAfter.jpg";

    public static IFormFile GetFakeImage()
    {
        // Create a new image instance
        var image = new Image<Rgba32>(100, 100);


        // Convert the image to a byte array
        using var ms = new MemoryStream();
        image.SaveAsJpeg(ms);
        var byteArray = ms.ToArray();

        // Wrap the byte array in a form file
        IFormFile formFile = new FormFile(ms, 0, byteArray.Length, "image/png", "fake_image.png");

        return formFile;
    }
}
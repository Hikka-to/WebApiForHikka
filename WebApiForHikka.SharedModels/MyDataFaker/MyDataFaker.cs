using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace WebApiForHikka.SharedModels.MyDataFaker;

public static class MyDataFaker
{
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
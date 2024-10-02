using Microsoft.AspNetCore.Http;
using SkiaSharp;
// using Image = SixLabors.ImageSharp.Image;

namespace WebApiForHikka.SharedFunction.Helpers.FileHelper;

public class FileHelper : IFileHelper
{
    public void DeleteFile(string[] path, string fileName)
    {
        try
        {
            File.Delete(Path.Combine(Directory.GetCurrentDirectory(), string.Join("\\", path)) +
                        fileName);
        }
        catch (DirectoryNotFoundException)
        {
        }
    }

    public void DeleteFile(string path)
    {
        try
        {
            File.Delete(path);
        }
        catch (DirectoryNotFoundException)
        {
        }
    }

    public byte[] GetFile(string path)
    {
        return File.ReadAllBytes(path);
    }

    public byte[] GetFile(string[] path, string fileName)
    {
        var targetDirectory =
            Path.Combine(Directory.GetCurrentDirectory(), string.Join("\\", path));
        var filePath = Path.Combine(targetDirectory, fileName);

        return File.ReadAllBytes(filePath);
    }

    public void OverrideFileImage(IFormFile file, string path)
    {
        using var inputStream = file.OpenReadStream();
        using var skBitmap = SKBitmap.Decode(inputStream);
        using var image = SKImage.FromBitmap(skBitmap);
        using var data = image.Encode(SKEncodedImageFormat.Webp, 80);
        using var stream = File.OpenWrite(path);
        data.SaveTo(stream);
    }

    public string UploadFileImage(IFormFile file, string[] path)
    {
        var targetDirectory =
            Path.Combine(Directory.GetCurrentDirectory(), string.Join("\\", path));
        Directory.CreateDirectory(targetDirectory);

        var fileNameWithoutExtension = Guid.NewGuid();
        var webPFileName = $"{fileNameWithoutExtension}.webp";
        var filePath = Path.Combine(targetDirectory, webPFileName);

        using var inputStream = file.OpenReadStream();
        using var skBitmap = SKBitmap.Decode(inputStream);
        using var image = SKImage.FromBitmap(skBitmap);
        using var data = image.Encode(SKEncodedImageFormat.Webp, 100);
        using var stream = File.OpenWrite(filePath);
        data.SaveTo(stream);

        return filePath;
    }


    public string UploadFileImage(IFormFile file, string[] path, string fileName)
    {
        var targetDirectory =
            Path.Combine(Directory.GetCurrentDirectory(), string.Join("\\", path));
        Directory.CreateDirectory(targetDirectory);

        var webPFileName = $"{fileName}.webp";
        var filePath = Path.Combine(targetDirectory, webPFileName);

        using var inputStream = file.OpenReadStream();
        using var skBitmap = SKBitmap.Decode(inputStream);
        using var image = SKImage.FromBitmap(skBitmap);
        using var data = image.Encode(SKEncodedImageFormat.Webp, 80);
        using var stream = File.OpenWrite(filePath);
        data.SaveTo(stream);

        return filePath;
    }

    public (int height, int width) GetHeightAndWidthOfImage(IFormFile file)
    {
        using var inputStream = file.OpenReadStream();
        using var skBitmap = SKBitmap.Decode(inputStream);
        return (skBitmap.Height, skBitmap.Width);
    }
}
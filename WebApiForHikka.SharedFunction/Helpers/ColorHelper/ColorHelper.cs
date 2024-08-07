using System.Drawing;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;

namespace WebApiForHikka.SharedFunction.Helpers.ColorHelper;

public class ColorHelper : IColorHelper
{
    public List<int> GetListOfColorsFromImage(IFormFile file)
    {
        if (file == null || file.Length == 0) throw new ArgumentException("No file was provided.");

        // Create a bitmap from the uploaded file
        using var memoryStream = new MemoryStream();
        file.CopyTo(memoryStream);
#pragma warning disable CA1416
        var image = new Bitmap(memoryStream);

        // Get the width and height of the image
        var width = image.Width;
        var height = image.Height;


        // Calculate coordinates for the middle left, middle right, top, and bottom pixels
        var middleLeft = new Point(0, height / 2);
        var middleRight = new Point(width - 1, height / 2);
        var topMiddle = new Point(width / 2, 0);
        var bottomMiddle = new Point(width / 2, height - 1);

        // Initialize a list to hold the RGB values
        var pixelColors = new List<int>();

        // Add the RGB values of the pixels to the list
        pixelColors.Add(
            Information.RGB(
                image.GetPixel(middleLeft.X, middleLeft.Y).R,
                image.GetPixel(middleLeft.X, middleLeft.Y).G,
                image.GetPixel(middleLeft.X, middleLeft.Y).B
            )
        );

        pixelColors.Add(Information.RGB(
            image.GetPixel(middleRight.X, middleRight.Y).R,
            image.GetPixel(middleRight.X, middleRight.Y).G,
            image.GetPixel(middleRight.X, middleRight.Y).B
        ));

        pixelColors.Add(Information.RGB(
            image.GetPixel(topMiddle.X, topMiddle.Y).R,
            image.GetPixel(topMiddle.X, topMiddle.Y).G,
            image.GetPixel(topMiddle.X, topMiddle.Y).B
        ));

        pixelColors.Add(Information.RGB(
            image.GetPixel(bottomMiddle.X, bottomMiddle.Y).R,
            image.GetPixel(bottomMiddle.X, bottomMiddle.Y).G,
            image.GetPixel(bottomMiddle.X, bottomMiddle.Y).B
        ));
#pragma warning restore CA1416

        return pixelColors;
    }
}
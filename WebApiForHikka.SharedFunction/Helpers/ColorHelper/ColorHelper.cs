using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;
using System.Drawing;

namespace WebApiForHikka.SharedFunction.Helpers.ColorHelper;

public class ColorHelper : IColorHelper
{
    public List<int> GetListOfColorsFromImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("No file was provided.");
        }

        // Create a bitmap from the uploaded file
        using var memoryStream = new MemoryStream();
        file.CopyTo(memoryStream);
        var image = new Bitmap(memoryStream);

        // Get the width and height of the image
        int width = image.Width;
        int height = image.Height;

        // Calculate coordinates for the middle left, middle right, top, and bottom pixels
        Point middleLeft = new Point(0, height / 2);
        Point middleRight = new Point(width - 1, height / 2);
        Point topMiddle = new Point(width / 2, 0);
        Point bottomMiddle = new Point(width / 2, height - 1);

        // Initialize a list to hold the RGB values
        List<int> pixelColors = new List<int>();

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

        return pixelColors;
    }
}

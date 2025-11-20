using System.Drawing;
using System.Drawing.Imaging;
using WzComparerR2.WzLib;

namespace CustomLoginThemeExtractor;

public static class Wz_PngExtension
{
    /// <summary>
    /// Save Wz_Png to PNG file
    /// </summary>
    public static void SaveToPng(this Wz_Png png, string path)
    {
        if (png == null)
            throw new ArgumentNullException(nameof(png));

        using (Bitmap bitmap = png.ExtractPng())
        {
            bitmap.Save(path, ImageFormat.Png);
        }
    }
}

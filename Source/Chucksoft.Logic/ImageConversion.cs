using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Chucksoft.Entities;
using System.Drawing.Drawing2D;

namespace Chucksoft.Logic
{
    public class ImageConversion
    {
        /// <summary>
        /// Resizes the specified bitmap.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        /// <param name="dimensions">The dimensions.</param>
        /// <returns></returns>
        public static byte[] Resize(byte[] bitmap, ImageDimension dimensions)
        {
            byte[] imageBytes;

            Bitmap image = ConvertByteArrayToBitmap(bitmap);
            image = Resize(image, dimensions);

            using (MemoryStream outStream = new MemoryStream())
            {
                image.Save(outStream, ImageFormat.Jpeg);
                outStream.Position = 0;
                imageBytes = new byte[outStream.Length];
                outStream.Read(imageBytes, 0, (int)outStream.Length);
            }

            return imageBytes;
        }

        /// <summary>
        /// Converts the byte array to bitmap.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns></returns>
        public static Bitmap ConvertByteArrayToBitmap(byte[] bytes)
        {
            Bitmap image;

            using (MemoryStream memStream = new MemoryStream(bytes))
            {
                image = new Bitmap(memStream);
            }

            return image;
        }

        /// <summary>
        /// Resizes the bitmap, pass in the new sizes
        /// </summary>
        /// <param name="bitmap">bitmap to be resized</param>
        /// <param name="dimensions">The dimensions.</param>
        /// <returns></returns>
        public static Bitmap Resize(Bitmap bitmap, ImageDimension dimensions)
        {
            Bitmap result = new Bitmap(dimensions.Width, dimensions.Height);
            using (Graphics graphic = Graphics.FromImage(result))
            {
                graphic.SmoothingMode = SmoothingMode.HighQuality;
                graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphic.CompositingQuality = CompositingQuality.HighQuality;
                graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;

                graphic.DrawImage(bitmap, 0, 0, dimensions.Width, dimensions.Height);
            }

            return result;
        }

    }
}

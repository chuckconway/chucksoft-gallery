using System.IO;

namespace Chucksoft.Silverlight.Controls
{
    public class File
    {
        /// <summary>
        /// retrieves the extension of the string passed it.
        /// </summary>
        /// <param name="file">name of the file</param>
        /// <returns>
        /// the value past the last '.' found in the string.
        /// </returns>
        public static string GetFileExtension(string file)
        {
            string extension = string.Empty;

            //if the input parameter is null or empty return the empty string.
            if (!string.IsNullOrEmpty(file))
            {
                string[] stringSegments = file.Split('.');
                extension = stringSegments[(stringSegments.Length - 1)];
            }

            return extension;    
        }

        /// <summary>
        /// Gets the byte array from stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        public static byte[] GetByteArrayFromStream(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];

            using (stream)
            {
                stream.Read(bytes, 0, (int)stream.Length);
            }

            return bytes;
        }
    }
}

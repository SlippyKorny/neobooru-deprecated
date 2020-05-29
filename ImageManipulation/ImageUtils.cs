using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ImageManipulation
{
    /// <summary>
    /// Contains a set of useful image related methods.
    /// </summary>
    public class ImageUtils
    {
        /// <summary>
        /// Extracts the image extension from the contentType.
        /// </summary>
        /// <param name="contentType">the content type of a file</param>
        /// <returns></returns>
        public static string ImgExtensionFromContentType(string contentType)
        {
            string[] splitter = contentType.Split('/');
            return splitter[1];
        }

        /// <summary>
        /// Gets md5hash from the specified file.
        /// </summary>
        /// <param name="filePath">Path to the file</param>
        /// <returns></returns>
        public static string HashFromFile(string filePath)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    return Encoding.UTF8.GetString(md5.ComputeHash(stream));
                }
            }
        }

        /// <summary>
        /// Returns the height and width of the give image.
        /// </summary>
        /// <param name="path">Path to the file</param>
        /// <returns></returns>
        public static (int, int) DimensionsOfImage(string path)
        {
            using (var input = File.OpenRead(path))
            {
                var image = Image.Load(input);
                return (width: image.Width, height: image.Height);
            }
        }
    }
}

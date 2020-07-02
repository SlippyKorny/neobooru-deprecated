using ImageManipulation.Exceptions;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ImageManipulation
{
    /// <summary>
    /// Class used for image saving and its manipulation.
    /// </summary>
    public class ImageFileManager : IDisposable
    {
        /// <summary>
        /// Base folder path of this image.
        /// </summary>
        private string _folder;

        /// <summary>
        /// Stream to the image.
        /// </summary>
        Stream _imgStream;

        /// <summary>
        /// Extension of the received image file.
        /// </summary>
        private string _extension;

        /// <summary>
        /// Guid used for the names of the created files.
        /// </summary>
        private Guid _fileGuid;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="folder">Base folder for the file manipulation</param>
        /// <param name="imgStream">Stream to the image</param>
        public ImageFileManager(string folder, Stream imgStream, string extension)
        {
            _folder = folder;
            _fileGuid = Guid.NewGuid();
            _extension = extension;
            _imgStream = imgStream;
            Image img = Image.Load(_imgStream);
            if (img.Height <= 300 || img.Width <= 300)
                throw new InvalidArtDimensionsException();
        }


        /// <summary>
        /// Saves the unedited image to file.
        /// </summary>
        /// <returns>Path to the large image file.</returns>
        public async Task<string> SaveLarge()
        {
            string filePath = _folder + _fileGuid.ToString() + "_large." + _extension;
            _imgStream.Position = 0; 
            Image img = Image.Load(_imgStream);
            img.Save(filePath);
            return filePath;
        }

        /// <summary>
        /// Saves the minimized image to file.
        /// </summary>
        /// <returns>Path to the minimized image file.</returns>
        public async Task<string> SaveNormal()
        {
            string filePath = _folder + _fileGuid.ToString() + ".jpeg";
            _imgStream.Position = 0;
            Image img = Image.Load(_imgStream);
            int height = img.Height, width = img.Width;
            int resizedWidth = width * 75 / 100, resizedHeight = height * 75 / 100;
            img.Mutate(i => i.Resize(resizedWidth, resizedHeight));
            using (Stream file = File.Create(filePath))
            {
                img.SaveAsJpeg(file);
            }
            return filePath;
        }

        /// <summary>
        /// Saves the cropped thumbnail for this image.
        /// </summary>
        /// <param name="x">X coordinate used for cropping</param>
        /// <param name="y">Y coordinate used for cropping</param>
        /// <returns>Path to the thumbnail image file.</returns>
        public async Task<string> SaveThumbnail(int x, int y)
        {
            string filePath = _folder + _fileGuid.ToString() + "_thumbnail." + _extension;
            _imgStream.Position = 0;
            Image img = Image.Load(_imgStream);
            int height = img.Height, width = img.Width;
            img.Mutate(i => i.Crop(new Rectangle(x, y, 300, 300)));
            using (Stream file = File.Create(filePath))
                img.SaveAsJpeg(file);
            return filePath;
        }

        /// <summary>
        /// Edits the image to be suitable for a profile picture.
        /// </summary>
        /// <returns>profile id</returns>
        /// <exception cref="InvalidArtDimensionsException">When an image width to height ratio is not equal 1</exception>
        public async Task<string> SavePfp(Guid guid)
        {
            string filePath = _folder + guid.ToString() + "_pfp." + _extension;
            _imgStream.Position = 0;
            Image img = Image.Load(_imgStream);
            if (img.Height / img.Width != 1 || img.Height < 400 )
                throw new InvalidArtDimensionsException();
            img.Mutate(i => i.Resize(400, 400));
            using (Stream file = File.Create(filePath))
                img.SaveAsJpeg(file);
            return filePath;
        }

        /// <summary>
        /// Edits the image to be suitable for a background.
        /// </summary>
        /// <param name="guid">profile id</param>
        /// <returns></returns>
        /// <exception cref="InvalidArtDimensionsException"></exception>
        public async Task<string> SaveBg(Guid guid)
        {
            string filePath = _folder + guid.ToString() + "_bg." + _extension;
            _imgStream.Position = 0;
            Image img = Image.Load(_imgStream);
            if (img.Height < 540 || img.Width < 1590)
                throw new InvalidArtDimensionsException();
            img.Mutate(i => i.Crop(1590, 540));
            using (Stream file = File.Create(filePath))
                img.SaveAsJpeg(file);
            return filePath;
        }

        /// <summary>
        /// Saves without any editions in the png format
        /// </summary>
        /// <returns></returns>
        public async Task<string> Save(Guid guid)
        {
            string filePath = _folder + guid.ToString() + _extension;
            _imgStream.Position = 0;
            Image img = Image.Load(_imgStream);
            using (Stream file = File.Create(filePath))
                img.SaveAsJpeg(file);
            return filePath;
        }

        /// <summary>
        /// Provides a mechanism for releasing unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _imgStream.Close();
        }
    }
}

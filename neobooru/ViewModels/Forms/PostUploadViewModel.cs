using Microsoft.AspNetCore.Http;
using neobooru.Models;
using neobooru.Utilities.Attributes;

namespace neobooru.ViewModels
{
    public class PostUploadViewModel
    {
        // [ValidImage(allowedExtensions: new string[] { "png", "jpg" })]
        public IFormFile File { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public string Source { get; set; }

        public Art.ArtRating Rating { get; set; }

        public string TagString { get; set; }
    }
}

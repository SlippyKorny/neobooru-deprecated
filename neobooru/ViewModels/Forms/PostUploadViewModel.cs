using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using neobooru.Models;
using neobooru.Utilities.Attributes;

namespace neobooru.ViewModels
{
    public class PostUploadViewModel
    {
        [ValidImage(allowedContentTypes: new string[] { "jpg", "jpeg", "pjpeg", "png", "x-png" })]
        [Required]
        public IFormFile File { get; set; }

        [Required]
        public string Name { get; set; }

        public string Author { get; set; }

        public string Source { get; set; }

        [Required]
        public Art.ArtRating Rating { get; set; }

        [Required]
        public string TagString { get; set; }
    }
}

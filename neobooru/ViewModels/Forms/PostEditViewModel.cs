using System.ComponentModel.DataAnnotations;
using neobooru.Models;

namespace neobooru.ViewModels.Forms
{
    public class PostEditViewModel
    {
        [Required]
        public string Name { get; set; }

        public string Author { get; set; }

        public string Source { get; set; }

        [Required]
        public Art.ArtRating Rating { get; set; }

        [Required]
        public string TagString { get; set; }
        
        public string PreviewUrl { get; set; }
        
        public string SecretId { get; set; }
    }
}
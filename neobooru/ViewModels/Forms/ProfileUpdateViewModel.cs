using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using neobooru.Utilities.Attributes;

namespace neobooru.ViewModels.Forms
{
    public class ProfileUpdateViewModel
    {
        [Required]
        public string Username { get; set; }
        
        [ValidImage(allowedContentTypes: new string[] { "jpg", "jpeg", "pjpeg", "png", "x-png" })]
        public IFormFile PfpImage { get; set; }
        
        [ValidImage(allowedContentTypes: new string[] { "jpg", "jpeg", "pjpeg", "png", "x-png" })]
        public IFormFile BgImage { get; set; }
        
        public string ProfileDescription { get; set; }
        
        public string Gender { get; set; }
    }
}
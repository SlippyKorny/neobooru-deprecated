using Microsoft.AspNetCore.Http;
using neobooru.Utilities.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace neobooru.ViewModels.Forms
{
    public class ArtistRegistrationViewModel
    {
        [Required]
        [Display(Name = "Name:")]
        public string Name { get; set; }

        [Required]
        [ValidImage(allowedExtensions: new string[] { "png", "jpg", "jpeg", "gif" })]
        [Display(Name = "Profile picture:")]
        public IFormFile Pfp { get; set; }

        [ValidImage(allowedExtensions: new string[] { "png", "jpg", "jpeg" })]
        [Display(Name = "Background image:")]
        public IFormFile BackgroundImage { get; set; }

        [Display(Name = "Country:")]
        public string Country { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "Facebook profile link:")]
        public string FacebookProfileUrl { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "Twitter profile link:")]
        public string TwitterProfileUrl { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail address:")]
        public string MailAddress { get; set; }

        [Display(Name = "Gender:")]
        public string Gender { get; set; }
        
        [Display(Name = "Date of birth:")]
        public DateTime BirthDate { get; set; }
    }
}

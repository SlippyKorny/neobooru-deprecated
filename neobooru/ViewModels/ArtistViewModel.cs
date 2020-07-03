using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using neobooru.Models;

namespace neobooru.ViewModels
{
    public class ArtistViewModel
    {
        public readonly string ArtistName;

        public readonly DateTime RegisteredAt;

        public readonly int ProfileViews;

        public readonly String PfpUrl;

        public readonly String BackgroundUrl;

        public readonly int Followers;

        public readonly string Country;

        public readonly string FacebookProfileUrl;

        public readonly string TwitterProfileUrl;

        [EmailAddress]
        public readonly string MailAddress;

        public readonly string Gender;

        public readonly DateTime BirthDate;

        public readonly List<ArtThumbnailViewModel> ArtThumbnails;

        private ArtistViewModel() { }

        public ArtistViewModel(Artist artist, List<ArtThumbnailViewModel> artThumbnails, int followers)
        {
            ArtistName = artist.ArtistName;
            RegisteredAt = artist.RegisteredAt;
            ProfileViews = artist.ProfileViews;
            PfpUrl = artist.PfpUrl;
            BackgroundUrl = artist.BackgroundImageUrl;
            Followers = followers;
            Country = artist.Country;
            FacebookProfileUrl = artist.FacebookProfileUrl;
            TwitterProfileUrl = artist.TwitterProfileUrl;
            MailAddress = artist.MailAddress;
            Gender = artist.Gender;
            BirthDate = artist.BirthDate;
            ArtThumbnails = artThumbnails;
        }
    }
}

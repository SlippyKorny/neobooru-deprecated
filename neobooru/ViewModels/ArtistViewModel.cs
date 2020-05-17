using System;
using System.Collections.Generic;
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

        public readonly int Followers;

        public readonly string Country;

        public readonly string FacebookProfileUrl;

        public readonly string TwitterProfileUrl;

        // TODO: Perhaps some kind of verification for e-mail addresses (ASP.NET Core probably has something built in for that)
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

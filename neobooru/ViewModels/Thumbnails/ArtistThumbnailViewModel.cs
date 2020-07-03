using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using neobooru.Models;

namespace neobooru.ViewModels
{
    public class ArtistThumbnailViewModel
    {
        public readonly string Id;
        
        public readonly string ArtistName;

        public readonly string PfpUrl;

        public readonly int NumberOfArts;

        public readonly DateTime RegistrationTime;

        public readonly int Subscriptions;

        public readonly int ProfileViews;

        private ArtistThumbnailViewModel() { }

        public ArtistThumbnailViewModel(Artist artist, int numOfArts, int numOfSubs)
        {
            Id = artist.Id.ToString();
            ArtistName = artist.ArtistName;
            PfpUrl = artist.PfpUrl;
            NumberOfArts = numOfArts;
            RegistrationTime = artist.RegisteredAt;
            Subscriptions = numOfSubs;
            ProfileViews = artist.ProfileViews;
        }
    }
}

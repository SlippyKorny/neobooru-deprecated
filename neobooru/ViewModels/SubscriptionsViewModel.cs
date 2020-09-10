using System.Collections.Generic;

namespace neobooru.ViewModels
{
    public class SubscriptionsViewModel
    {
        public string Username { get; set; }

        public string ProfileId { get; set; }

        public string PfpUrl { get; set; }

        public string BackgroundUrl { get; set; }

        public string Description { get; set; }

        public List<ArtistThumbnailViewModel> Thumbnails { get; set; }
    }
}
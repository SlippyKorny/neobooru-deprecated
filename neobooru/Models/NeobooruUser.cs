using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace neobooru.Models
{
    public class NeobooruUser : IdentityUser
    {
        public DateTime RegisteredOn { get; set; }

        public string PfpUrl { get; set; }
        
        public string PfpThumbnailUrl { get; set; }

        public string ProfileDescription { get; set; }

        public ICollection<Art> UploadedArts { get; set; }

        public ICollection<ArtLike> ArtLikes { get; set; }

        public ICollection<ArtComment> ArtComments { get; set; }

        public ICollection<Pool> Pools { get; set; }

        public ICollection<ArtistSubscription> Subscriptions { get; set; }

        public ICollection<HelpEntrySection> CreatedHelpEntrySections { get; set; }

        public ICollection<HelpEntry> CreatedHelpEntries { get; set; }

        public ICollection<Tag> CreatedTags { get; set; }
    }
}

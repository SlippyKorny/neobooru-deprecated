using System;
using System.Collections.Generic;
using System.Linq;
using neobooru.Models;

namespace neobooru.ViewModels
{
    public class ProfileViewModel
    {
        public readonly string Username;

        public readonly DateTime RegisteredAt;

        public readonly int ProfileViews;

        public readonly String PfpUrl;

        public readonly String BackgroundUrl;

        public readonly string Gender;

        public readonly string Descritpion;

        public readonly List<ArtThumbnailViewModel> RecentlyUploaded;

        public ProfileViewModel(NeobooruUser user, List<ArtThumbnailViewModel> uploaded)
        {
            Username = user.UserName;
            RegisteredAt = user.RegisteredOn;
            ProfileViews = user.Views; 
            PfpUrl = user.PfpUrl ?? "/img/prototyping/avatar.png";
            BackgroundUrl = "/img/prototyping/backgrounds/1590x540p Background Placeholder Image.png";
            Gender = user.Gender ?? "Unknown";
            Descritpion = user.ProfileDescription ?? "";
            RecentlyUploaded = uploaded;
        }
    }
}
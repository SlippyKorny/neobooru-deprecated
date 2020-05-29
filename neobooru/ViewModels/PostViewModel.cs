using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Language;
using neobooru.Models;

namespace neobooru.ViewModels
{
    public class PostViewModel
    {
        public readonly Guid PostId;

        public readonly string PostName;

        public readonly string FileUrl;

        public readonly string LargeFileUrl;

        public readonly DateTime UploadedAt;

        public readonly string UploaderName;

        public readonly string Size;

        public readonly string Source;

        public readonly Art.ArtRating Rating;

        public readonly string ArtistName;

        public readonly List<string> Tags;

        public readonly ArtistThumbnailViewModel ArtistThumbnail;

        private PostViewModel() { }

        public PostViewModel(Art art, Artist artist, int numOfArts, int numOfSubs)
        {
            ArtistThumbnail = new ArtistThumbnailViewModel(artist, numOfArts, numOfSubs);

            PostId = art.Id;
            PostName = art.Name;
            FileUrl = art.FileUrl;
            LargeFileUrl = art.LargeFileUrl;
            UploadedAt = art.CreatedAt;
            UploaderName = art.Uploader.UserName;
            Size = art.Width + "x" + art.Height;
            Source = art.Source;
            Rating = art.Rating;
            ArtistName = art.Author.ArtistName;
            Tags = new List<string>();

            foreach (Tag tag in art.Tags)
                Tags.Add(tag.TagString);
        }
    }
}

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

        public readonly List<CommentViewModel> Comments;

        public readonly List<string> Tags;

        public readonly ArtistThumbnailViewModel ArtistThumbnail;

        public readonly bool ArtLiked;

        private PostViewModel() { }

        public PostViewModel(Art art, int numOfArts, int numOfSubs, List<string> tags, List<CommentViewModel> comments,
            bool liked)
        {
            ArtistThumbnail = new ArtistThumbnailViewModel(art.Author, numOfArts, numOfSubs);

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
            Tags = tags;
            ArtLiked = liked;
            Comments = comments;

            // foreach (Tag tag in art.Tags)
            // foreach (Tag tag in art.Tags.Select(to => to.Tag))
            // Tags.Add(tag.TagString);
        }
    }
}

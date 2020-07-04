using neobooru.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace neobooru.Models
{
    public class Art
    {
        [Key]
        public Guid Id { get; set; }

        public string FileUrl { get; set; }

        public string PreviewFileUrl { get; set; }

        [Required]
        public string LargeFileUrl { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        [Required]
        public NeobooruUser Uploader { get; set; }

        public string Name { get; set; }

        public Artist Author { get; set; }

        public int Stars { get; set; }

        public string Source { get; set; }

        public string Md5Hash { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public int FileSize { get; set; }

        public ArtRating Rating { get; set; }

        public ICollection<TagOccurrence> Tags { get; set; }

        public ICollection<ArtComment> Comments { get; set; }
        
        public ICollection<ArtLike> Likes { get; set; }

        public enum ArtRating
        {
            [Description("Safe For Work")]
            Safe,
            [Description("Questionable")]
            Questionable,
            [Description("Not Safe For Work")]
            NotSafe,
            [Description("Offensive")]
            Offensive
        }
    }
}

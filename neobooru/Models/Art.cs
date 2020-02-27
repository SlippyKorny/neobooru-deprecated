using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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

        // TODO: Uploader ID or reference to the model
        public string Uploader;

        public string Name { get; set; }

        public Artist Author { get; set; }

        public int Stars { get; set; }

        public string Source { get; set; }

        public string Md5Hash { get; set; }

        public float Height { get; set; }

        public float Width { get; set; }

        public int FileSize { get; set; }

        public ArtRating Rating { get; set; }

        public virtual ICollection<Tag> tags { get; set; }

        public virtual ICollection<Comment> comments { get; set; }


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

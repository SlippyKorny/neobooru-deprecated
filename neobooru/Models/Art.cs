using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace neobooru.Models
{
    public class Art
    {
        [Key]
        public Guid Id { get; set; }

        public String FileUrl { get; set; }

        public String PreviewFileUrl { get; set; }

        [Required]
        public String LargeFileUrl { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        // TODO: Uploader ID or reference to the model

        public string Name { get; set; }

        public Artist Author { get; set; }

        public int Stars { get; set; }

        public String Source { get; set; }

        public String Md5Hash { get; set; }

        public float Height { get; set; }

        public float Width { get; set; }

        public int FileSize { get; set; }

        public virtual ICollection<Tag> tags { get; set; }

        public virtual ICollection<Comment> comments { get; set; }
    }
}

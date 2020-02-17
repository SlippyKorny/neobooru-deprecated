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
        public Guid id { get; set; }

        public String fileUrl { get; set; }

        public String previewFileUrl { get; set; }

        [Required]
        public String largeFileUrl { get; set; }

        [Required]
        public DateTime createdAt { get; set; }

        public DateTime updatedAt { get; set; }

        // TODO: Uploader ID or reference to the model

        public Artist author { get; set; }

        public int stars { get; set; }

        public String source { get; set; }

        public String md5Hash { get; set; }

        public float height { get; set; }

        public float width { get; set; }

        public int fileSize { get; set; }

        public virtual ICollection<Tag> tags { get; set; }

        public virtual ICollection<Comment> comments { get; set; }
    }
}

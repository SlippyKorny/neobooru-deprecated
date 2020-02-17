using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace neobooru.Models
{
    public class Artist
    {
        [Key]
        public Guid id { get; set; }

        [Required]
        public String artistName { get; set; }

        [Required]
        public DateTime registeredAt { get; set; }

        public int profileViews { get; set; }

        // TODO: One artist to many users relation - subscriptions

        public String largePfpUrl { get; set; }

        [Required]
        public String pfpUrl { get; set; }

        public String previewPfpUrl { get; set; }
    }
}

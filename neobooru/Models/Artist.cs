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
        public Guid Id { get; set; }

        [Required]
        public String ArtistName { get; set; }

        [Required]
        public DateTime RegisteredAt { get; set; }

        public int ProfileViews { get; set; }

        // TODO: One artist to many users relation - subscriptions

        public String LargePfpUrl { get; set; }

        [Required]
        public String PfpUrl { get; set; }

        public String PreviewPfpUrl { get; set; }
    }
}

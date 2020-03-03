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
        public string ArtistName { get; set; }

        [Required]
        public DateTime RegisteredAt { get; set; }

        public int ProfileViews { get; set; }

        public string LargePfpUrl { get; set; }

        [Required]
        public string PfpUrl { get; set; }

        public string PreviewPfpUrl { get; set; }

        public string Country { get; set; }

        public string FacebookProfileUrl { get; set; }

        public string TwitterProfileUrl { get; set; }

        public string MailAddress { get; set; }

        public string Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public string Occupation { get; set; }
    }
}

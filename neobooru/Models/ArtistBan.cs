using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace neobooru.Models
{
    public class ArtistBan
    {
        [Key]
        public Guid id { get; set; }

        [Required]
        public DateTime BanDate { get; set; }

        [Required]
        public TimeSpan BanDuration { get; set; }

        [Required] 
        public Artist BannedArtist { get; set; }
    }
}

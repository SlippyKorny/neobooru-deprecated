using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace neobooru.Models
{
    public class ArtLike
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public NeobooruUser User { get; set; }

        [Required]
        public Art LikedArt { get; set; }
    }
}

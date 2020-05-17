using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace neobooru.Models
{
    public class ArtistSubscription
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public NeobooruUser Subscriber { get; set; }

        [Required]
        public Artist Artist { get; set; }

        [Required]
        public DateTime SubscribedOn { get; set; }
    }
}

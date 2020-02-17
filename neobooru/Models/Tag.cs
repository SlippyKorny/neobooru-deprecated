using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace neobooru.Models
{
    public class Tag
    {
        [Key]
        public Guid id { get; set; }

        [Required]
        public string tag { get; set; }

        [Required]
        public DateTime addedAt { get; set; }

        // [Required]
        // public 
        // TODO: User reference - who added it
    }
}

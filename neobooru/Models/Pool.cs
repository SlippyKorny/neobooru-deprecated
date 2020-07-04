using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace neobooru.Models
{
    public class Pool
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public String PoolName { get; set; }

        [Required]
        public NeobooruUser Creator { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public ICollection<Art> Arts { get; set; }
    }
}

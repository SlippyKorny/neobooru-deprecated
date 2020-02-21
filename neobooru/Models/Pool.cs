using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace neobooru.Models
{
    public class Pool
    {
        [Key]
        public Guid id { get; set; }

        public String PoolName { get; set; }

        // TODO: One to many relation with users

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public ICollection<Art> Arts { get; set; }
    }
}

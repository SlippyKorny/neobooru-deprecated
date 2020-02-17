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

        public String poolName { get; set; }

        // TODO: One to many relation with users

        [Required]
        public DateTime createdAt { get; set; }

        public DateTime updatedAt { get; set; }

        public ICollection<Art> arts { get; set; }
    }
}

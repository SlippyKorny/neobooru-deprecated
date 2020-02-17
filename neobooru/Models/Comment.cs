using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace neobooru.Models
{
    public class Comment
    {
        [Key]
        public Guid key { get; set; }

        // TODO: Reference to user

        [Required]
        public string comment { get; set; }

        [Required]
        public DateTime commentedOn { get; set; }

        public DateTime editedOn { get; set; }

        public int plusVotes { get; set; }

        public int minusVotes { get; set; }

        public Art commentedArt { get; set; }
    }
}

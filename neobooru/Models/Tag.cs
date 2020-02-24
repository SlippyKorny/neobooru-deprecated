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
        public Guid Id { get; set; }

        [Required]
        public string TagString { get; set; }

        [Required]
        public DateTime AddedAt { get; set; }

        public TagType Type { get; set; }

        public enum TagType
        {
            Character,
            Series,
            Metadata,
            Classic
        }

        // [Required]
        // public 
        // TODO: User reference - who added it
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace neobooru.Models
{
    public class HelpEntry
    {
        [Key]
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        [Required]
        public NeobooruUser Creator { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public HelpEntrySection ParentSection { get; set; }
    }
}

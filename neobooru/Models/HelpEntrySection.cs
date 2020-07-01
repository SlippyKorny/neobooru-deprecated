using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace neobooru.Models
{
    public class HelpEntrySection
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string SectionName { get; set; }

        public string SectionDescription { get; set; }

        public ICollection<HelpEntry> HelpEntries { get; set; }

        [Required]
        public NeobooruUser Creator { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }
    }
}

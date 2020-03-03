using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

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
        public IdentityUser Creator { get; set; }

        public IdentityUser Updater { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }
    }
}

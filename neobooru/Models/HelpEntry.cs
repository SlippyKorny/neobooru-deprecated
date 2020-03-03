using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace neobooru.Models
{
    public class HelpEntry
    {
        [Key]
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        [Required]
        public IdentityUser Creator { get; set; }

        public IdentityUser Updater { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        [Required]
        public HelpEntrySection ParentSection { get; set; }
    }
}

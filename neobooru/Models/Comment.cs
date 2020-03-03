using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace neobooru.Models
{
    public class Comment
    {
        [Key]
        public Guid Id { get; set; }

        // TODO: Reference to user
        [Required]
        public IdentityUser User { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime CommentedOn { get; set; }

        public DateTime EditedOn { get; set; }

        public int PlusVotes { get; set; }

        public int MinusVotes { get; set; }

        public Art CommentedArt { get; set; }
    }
}

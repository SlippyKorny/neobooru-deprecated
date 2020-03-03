﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace neobooru.Models
{
    public class Pool
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public String PoolName { get; set; }

        [Required]
        public IdentityUser Creator { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<Art> Arts { get; set; }
    }
}

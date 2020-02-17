using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace neobooru.Models
{
    public class ApplicationDbContext : IdentityDbContext
    {
        private DbSet<Art> arts { get; set; }

        private DbSet<Artist> artists { get; set; }

        private DbSet<Pool> pools { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
    }
}

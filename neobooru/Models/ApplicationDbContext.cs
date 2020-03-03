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
        public virtual DbSet<Art> Arts { get; set; }

        public virtual DbSet<Artist> Artists { get; set; }

        public virtual DbSet<ArtistBan> ArtistBans { get; set; }

        public virtual DbSet<ArtistSubscription> ArtistSubscriptions { get; set; }

        public virtual DbSet<ArtLike> ArtLikes { get; set; }

        public virtual DbSet<Comment> Comments { get; set; }

        public virtual DbSet<HelpEntry> HelpEntries { get; set; }

        public virtual DbSet<Pool> Pools { get; set; }

        public virtual DbSet<Tag> Tags { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
    }
}

using System;
using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using neobooru;

namespace neobooru.Models
{
    public partial class NeobooruDataContext : IdentityDbContext
    {
        public virtual DbSet<Art> Arts { get; set; }

        public virtual DbSet<ArtComment> ArtComments { get; set; }

        public virtual DbSet<Artist> Artists { get; set; }

        public virtual DbSet<ArtistBan> ArtistBans { get; set; }

        public virtual DbSet<ArtistSubscription> ArtistSubscriptions { get; set; }

        public virtual DbSet<ArtLike> ArtLikes { get; set; }

        public virtual DbSet<HelpEntry> HelpEntries { get; set; }

        public virtual DbSet<HelpEntrySection> HelpEntrySections { get; set; }

        public virtual DbSet<NeobooruUser> NeobooruUsers { get; set; }

        public virtual DbSet<Pool> Pools { get; set; }

        public virtual DbSet<Tag> Tags { get; set; }
        
        public NeobooruDataContext()
        {
        }
        
        public NeobooruDataContext(DbContextOptions<NeobooruDataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.NoAction;

            base.OnModelCreating(builder);
        }

//         public NeobooruDataContext(DbContextOptions<NeobooruDataContext> options)
//             : base(options)
//         {
//         }
//
//         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//         {
//             if (!optionsBuilder.IsConfigured)
//             {
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                 optionsBuilder.UseSqlServer("Server=localhost;Database=neobooru;user=sa;password=#Password12;Trusted_Connection=False;MultipleActiveResultSets=True;ConnectRetryCount=0");
//             }
//         }
//
//         protected override void OnModelCreating(ModelBuilder modelBuilder)
//         {
//             OnModelCreatingPartial(modelBuilder);
//         }
//
//         partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

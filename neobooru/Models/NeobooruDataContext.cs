using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
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
            // >>> Relations configuration <<<
            // Arts -> TagOccurrence <- Tags
            builder.Entity<TagOccurrence>().HasKey(to => new {to.ArtId, to.TagId});
            builder.Entity<TagOccurrence>().HasOne(to => to.Art)
                .WithMany(a => a.Tags).HasForeignKey(to => to.ArtId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.Entity<TagOccurrence>().HasOne(to => to.Tag)
                .WithMany(t => t.Occurrences).HasForeignKey(to => to.TagId)
                .OnDelete(DeleteBehavior.SetNull);
            // Art <-(many)- Artist 
            // builder.Entity<Art>().HasOne(ar => ar.Author)
            //     .WithMany(au => au.Arts);
            

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.NoAction;

            base.OnModelCreating(builder);
        }
    }
}

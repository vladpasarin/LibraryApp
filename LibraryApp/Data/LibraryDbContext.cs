using LibraryApp.Entities;
using LibraryApp.Entities.Assets.Tags;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext() { }

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

        public virtual DbSet<Asset> Assets { get; set; }
        public virtual DbSet<AudioBook> AudioBooks { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<EBook> EBooks { get; set; }
        public virtual DbSet<AssetTag> AssetTags { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<AvailabilityStatus> AvailabilityStatuses { get; set; }
        public virtual DbSet<Badge> Badges { get; set; }
        public virtual DbSet<Challenge> Challenges { get; set; }
        public virtual DbSet<Checkout> Checkouts { get; set; }
        public virtual DbSet<CheckoutHistory> CheckoutHistories { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Discussion> Discussions { get; set; }
        public virtual DbSet<Hold> Holds { get; set; }
        public virtual DbSet<LibraryCard> LibraryCards { get; set; }
        public virtual DbSet<ReadingLog> ReadingLogs { get; set; }
        public virtual DbSet<ReplyTo> ReplyTos { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Bookmark> Bookmarks { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            LoadDefaultAssetStatuses(builder);
            LinkAssets(builder);
            LinkAssetTags(builder);
            LinkOneToOne(builder);
            LinkManyToMany(builder);

            base.OnModelCreating(builder);
        }

        private static void LinkAssets(ModelBuilder builder)
        {
            // One to One
            builder.Entity<Book>()
                .HasOne(b => b.Asset)
                .WithOne()
                .HasForeignKey<Book>(b => b.AssetId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<EBook>()
                .HasOne(eb => eb.Asset)
                .WithOne()
                .HasForeignKey<EBook>(eb => eb.AssetId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<AudioBook>().HasOne(ab => ab.Asset).WithOne()
                .HasForeignKey<AudioBook>(ab => ab.AssetId)
                .OnDelete(DeleteBehavior.Cascade);

            // One to Many
            builder.Entity<Asset>().HasOne(a => a.AvailabilityStatus)
                .WithMany(a => a.Assets).HasForeignKey(a => a.AvailabilityStatusId);
        }

        private static void LinkAssetTags(ModelBuilder builder)
        {
            builder.Entity<AssetTag>().HasOne(at => at.Asset)
                .WithMany(a => a.AssetTags).HasForeignKey(at => at.AssetId);

            builder.Entity<AssetTag>().HasOne(at => at.Tag)
                .WithMany(t => t.AssetTags).HasForeignKey(at => at.TagId);
        }

        private static void LinkOneToOne(ModelBuilder builder)
        {
            builder.Entity<User>().HasOne(u => u.LibraryCard).WithOne()
                .HasForeignKey<User>(u => u.LibraryCardId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Badge>().HasOne(b => b.Challenge)
                .WithOne().HasForeignKey<Badge>(b => b.ChallengeId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private static void LinkManyToMany(ModelBuilder builder)
        {
            builder.Entity<Bookmark>()
                .HasOne(b => b.Asset)
                .WithMany(a => a.Bookmarks)
                .HasForeignKey(b => b.AssetId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<Bookmark>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookmarks)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Rating>()
                .HasOne(r => r.Asset)
                .WithMany(a => a.AssetRatings)
                .HasForeignKey(r => r.AssetId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Rating>()
                .HasOne(r => r.User)
                .WithMany(u => u.UserRatings)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private static void LoadDefaultAssetStatuses(ModelBuilder builder)
        {
            var defaultStatuses = new List<AvailabilityStatus>
            {
                new AvailabilityStatus
                {
                    Id = 1,
                    Name = "Available",
                    Description = "The item is available"
                },
                new AvailabilityStatus
                {
                    Id = 2,
                    Name = "On Hold",
                    Description = "The item is currently on hold"
                },
                new AvailabilityStatus
                {
                    Id = 3,
                    Name = "Unavailable",
                    Description = "The item is unavailable"
                },
            };

            builder.Entity<AvailabilityStatus>().HasData(defaultStatuses);
        }
    }
}

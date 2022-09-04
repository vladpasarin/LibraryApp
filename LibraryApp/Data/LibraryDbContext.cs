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
        public virtual DbSet<UserChallenge> UserChallenges { get; set; }
        //public virtual DbSet<ChallengeType> ChallengeTypes { get; set; }
        public virtual DbSet<Goal> Goals { get; set; }
        public virtual DbSet<GoalType> GoalTypes { get; set; }
        public virtual DbSet<Quote> Quotes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            LoadDefaultAssetStatuses(builder);
            LinkAssets(builder);
            LinkAssetTags(builder);
            LinkOneToOne(builder);
            LinkManyToMany(builder);
            LinkOneToMany(builder);

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
            
            /*
            builder.Entity<ChallengeType>()
                .HasOne(ct => ct.Challenge)
                .WithOne(c => c.ChallengeType)
                .HasForeignKey<Challenge>(c => c.ChallengeTypeId);
            */
        }

        private static void LinkOneToMany(ModelBuilder builder)
        {
            builder.Entity<Goal>()
                .HasOne(g => g.GoalType)
                .WithMany(gt => gt.Goals)
                .HasForeignKey(g => g.GoalTypeId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Goal>()
                .HasOne(g => g.User)
                .WithMany(u => u.UserGoals)
                .HasForeignKey(g => g.UserId)
                .OnDelete(DeleteBehavior.Cascade);
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

            builder.Entity<UserChallenge>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.UserChallenges)
                .HasForeignKey(uc => uc.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserChallenge>()
                .HasOne(uc => uc.Challenge)
                .WithMany(c => c.UserChallenges)
                .HasForeignKey(uc => uc.ChallengeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Quote>()
                .HasOne(q => q.User)
                .WithMany(u => u.UserQuotes)
                .HasForeignKey(q => q.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Quote>()
                .HasOne(q => q.Book)
                .WithMany(b => b.BookQuotes)
                .HasForeignKey(q => q.BookId)
                .OnDelete(DeleteBehavior.Restrict);
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

            var defaultChallenges = new List<Challenge>
            {
                new Challenge
                {
                    Id = 1,
                    Name = "Newbie Reader",
                    Description = "Borrow your first book!"
                },

                new Challenge
                {
                    Id = 2,
                    Name = "Bookmark Enthusiast",
                    Description = "Bookmark 3 or more books!"
                },

                new Challenge
                {
                    Id = 3,
                    Name = "Opinionated Reader",
                    Description = "Rate 3 or more books!"
                }
            };

            var defaultGoalTypes = new List<GoalType>
            {
                new GoalType
                {
                    Id = 1,
                    Name = "Newbie reader",
                    Description = "Borrow a set number of books"
                },
                
                new GoalType
                {
                    Id = 2,
                    Name = "Bookmark Enjoyer",
                    Description = "Bookmark a set number of books"
                },

                new GoalType
                {
                    Id = 3,
                    Name = "Opinionated Reader",
                    Description = "Rate a set number of books!"
                },

                new GoalType
                {
                    Id = 4,
                    Name = "User Defined",
                    Description = String.Empty
                }
            };

            builder.Entity<AvailabilityStatus>().HasData(defaultStatuses);
            builder.Entity<Challenge>().HasData(defaultChallenges);
            builder.Entity<GoalType>().HasData(defaultGoalTypes);
        }
    }
}

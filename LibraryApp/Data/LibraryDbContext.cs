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
    }
}

﻿// <auto-generated />
using System;
using LibraryApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LibraryApp.Migrations
{
    [DbContext(typeof(LibraryDbContext))]
    partial class LibraryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("LibraryApp.Entities.Asset", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AvailabilityStatusId")
                        .HasColumnType("integer");

                    b.Property<double>("Cost")
                        .HasColumnType("double precision");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AvailabilityStatusId");

                    b.ToTable("Assets");
                });

            modelBuilder.Entity("LibraryApp.Entities.Assets.Tags.AssetTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AssetId")
                        .HasColumnType("integer");

                    b.Property<int>("TagId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AssetId");

                    b.HasIndex("TagId");

                    b.ToTable("AssetTags");
                });

            modelBuilder.Entity("LibraryApp.Entities.Assets.Tags.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("LibraryApp.Entities.AudioBook", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ASIN")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("AssetId")
                        .HasColumnType("integer");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DeweyIndex")
                        .HasColumnType("text");

                    b.Property<string>("Edition")
                        .HasColumnType("text");

                    b.Property<string>("Language")
                        .HasColumnType("text");

                    b.Property<int>("LengthMinutes")
                        .HasColumnType("integer");

                    b.Property<int>("PublicationYear")
                        .HasColumnType("integer");

                    b.Property<string>("Publisher")
                        .HasColumnType("text");

                    b.Property<string>("Summary")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AssetId")
                        .IsUnique();

                    b.ToTable("AudioBooks");
                });

            modelBuilder.Entity("LibraryApp.Entities.AvailabilityStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("AvailabilityStatuses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "The item is available",
                            Name = "Available"
                        },
                        new
                        {
                            Id = 2,
                            Description = "The item is currently on hold",
                            Name = "On Hold"
                        },
                        new
                        {
                            Id = 3,
                            Description = "The item is unavailable",
                            Name = "Unavailable"
                        });
                });

            modelBuilder.Entity("LibraryApp.Entities.Badge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("ChallengeId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateAcquired")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ChallengeId")
                        .IsUnique();

                    b.ToTable("Badges");
                });

            modelBuilder.Entity("LibraryApp.Entities.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AssetId")
                        .HasColumnType("integer");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DeweyIndex")
                        .HasColumnType("text");

                    b.Property<string>("Edition")
                        .HasColumnType("text");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Language")
                        .HasColumnType("text");

                    b.Property<int>("PublicationYear")
                        .HasColumnType("integer");

                    b.Property<string>("Publisher")
                        .HasColumnType("text");

                    b.Property<string>("Summary")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AssetId")
                        .IsUnique();

                    b.ToTable("Books");
                });

            modelBuilder.Entity("LibraryApp.Entities.Bookmark", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AssetId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AssetId");

                    b.HasIndex("UserId");

                    b.ToTable("Bookmarks");
                });

            modelBuilder.Entity("LibraryApp.Entities.Challenge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Completed")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("DateCompleted")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DateStarted")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Challenges");
                });

            modelBuilder.Entity("LibraryApp.Entities.Checkout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("AssetId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CheckedOutSince")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("CheckedOutUntil")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("LibraryCardId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AssetId");

                    b.HasIndex("LibraryCardId");

                    b.ToTable("Checkouts");
                });

            modelBuilder.Entity("LibraryApp.Entities.CheckoutHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("AssetId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("CheckedIn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("CheckedOut")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("LibraryCardId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AssetId");

                    b.HasIndex("LibraryCardId");

                    b.ToTable("CheckoutHistories");
                });

            modelBuilder.Entity("LibraryApp.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("DiscussionId")
                        .HasColumnType("integer");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DiscussionId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("LibraryApp.Entities.Discussion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("AssetId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AssetId");

                    b.HasIndex("UserId");

                    b.ToTable("Discussions");
                });

            modelBuilder.Entity("LibraryApp.Entities.EBook", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ASIN")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("AssetId")
                        .HasColumnType("integer");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DeweyIndex")
                        .HasColumnType("text");

                    b.Property<string>("Edition")
                        .HasColumnType("text");

                    b.Property<string>("Language")
                        .HasColumnType("text");

                    b.Property<int>("PublicationYear")
                        .HasColumnType("integer");

                    b.Property<string>("Publisher")
                        .HasColumnType("text");

                    b.Property<double>("Size")
                        .HasColumnType("double precision");

                    b.Property<string>("Summary")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AssetId")
                        .IsUnique();

                    b.ToTable("EBooks");
                });

            modelBuilder.Entity("LibraryApp.Entities.Hold", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("AssetId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("HoldPlaced")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("HoldValidUntil")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("LibraryCardId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("AssetId");

                    b.HasIndex("LibraryCardId");

                    b.ToTable("Holds");
                });

            modelBuilder.Entity("LibraryApp.Entities.LibraryCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<double>("CurrentFees")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("DateIssued")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("LibraryCards");
                });

            modelBuilder.Entity("LibraryApp.Entities.ReadingLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DateLogged")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("NumberOfPages")
                        .HasColumnType("integer");

                    b.Property<string>("ShortNote")
                        .HasColumnType("text");

                    b.Property<int>("TimeMinutes")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ReadingLogs");
                });

            modelBuilder.Entity("LibraryApp.Entities.ReplyTo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("CommentId")
                        .HasColumnType("integer");

                    b.Property<int?>("ResponseId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CommentId");

                    b.HasIndex("ResponseId");

                    b.ToTable("ReplyTos");
                });

            modelBuilder.Entity("LibraryApp.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("LibraryCardId")
                        .HasColumnType("integer");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("bytea");

                    b.Property<string>("PhoneNr")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("LibraryCardId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LibraryApp.Entities.Asset", b =>
                {
                    b.HasOne("LibraryApp.Entities.AvailabilityStatus", "AvailabilityStatus")
                        .WithMany("Assets")
                        .HasForeignKey("AvailabilityStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AvailabilityStatus");
                });

            modelBuilder.Entity("LibraryApp.Entities.Assets.Tags.AssetTag", b =>
                {
                    b.HasOne("LibraryApp.Entities.Asset", "Asset")
                        .WithMany("AssetTags")
                        .HasForeignKey("AssetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LibraryApp.Entities.Assets.Tags.Tag", "Tag")
                        .WithMany("AssetTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Asset");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("LibraryApp.Entities.AudioBook", b =>
                {
                    b.HasOne("LibraryApp.Entities.Asset", "Asset")
                        .WithOne()
                        .HasForeignKey("LibraryApp.Entities.AudioBook", "AssetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Asset");
                });

            modelBuilder.Entity("LibraryApp.Entities.Badge", b =>
                {
                    b.HasOne("LibraryApp.Entities.Challenge", "Challenge")
                        .WithOne()
                        .HasForeignKey("LibraryApp.Entities.Badge", "ChallengeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Challenge");
                });

            modelBuilder.Entity("LibraryApp.Entities.Book", b =>
                {
                    b.HasOne("LibraryApp.Entities.Asset", "Asset")
                        .WithOne()
                        .HasForeignKey("LibraryApp.Entities.Book", "AssetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Asset");
                });

            modelBuilder.Entity("LibraryApp.Entities.Bookmark", b =>
                {
                    b.HasOne("LibraryApp.Entities.Asset", "Asset")
                        .WithMany("Bookmarks")
                        .HasForeignKey("AssetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LibraryApp.Entities.User", "User")
                        .WithMany("Bookmarks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Asset");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LibraryApp.Entities.Challenge", b =>
                {
                    b.HasOne("LibraryApp.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LibraryApp.Entities.Checkout", b =>
                {
                    b.HasOne("LibraryApp.Entities.Asset", "Asset")
                        .WithMany()
                        .HasForeignKey("AssetId");

                    b.HasOne("LibraryApp.Entities.LibraryCard", "LibraryCard")
                        .WithMany("Checkouts")
                        .HasForeignKey("LibraryCardId");

                    b.Navigation("Asset");

                    b.Navigation("LibraryCard");
                });

            modelBuilder.Entity("LibraryApp.Entities.CheckoutHistory", b =>
                {
                    b.HasOne("LibraryApp.Entities.Asset", "Asset")
                        .WithMany()
                        .HasForeignKey("AssetId");

                    b.HasOne("LibraryApp.Entities.LibraryCard", "LibraryCard")
                        .WithMany()
                        .HasForeignKey("LibraryCardId");

                    b.Navigation("Asset");

                    b.Navigation("LibraryCard");
                });

            modelBuilder.Entity("LibraryApp.Entities.Comment", b =>
                {
                    b.HasOne("LibraryApp.Entities.Discussion", "Discussion")
                        .WithMany()
                        .HasForeignKey("DiscussionId");

                    b.HasOne("LibraryApp.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Discussion");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LibraryApp.Entities.Discussion", b =>
                {
                    b.HasOne("LibraryApp.Entities.Asset", "Asset")
                        .WithMany()
                        .HasForeignKey("AssetId");

                    b.HasOne("LibraryApp.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Asset");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LibraryApp.Entities.EBook", b =>
                {
                    b.HasOne("LibraryApp.Entities.Asset", "Asset")
                        .WithOne()
                        .HasForeignKey("LibraryApp.Entities.EBook", "AssetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Asset");
                });

            modelBuilder.Entity("LibraryApp.Entities.Hold", b =>
                {
                    b.HasOne("LibraryApp.Entities.Asset", "Asset")
                        .WithMany()
                        .HasForeignKey("AssetId");

                    b.HasOne("LibraryApp.Entities.LibraryCard", "LibraryCard")
                        .WithMany()
                        .HasForeignKey("LibraryCardId");

                    b.Navigation("Asset");

                    b.Navigation("LibraryCard");
                });

            modelBuilder.Entity("LibraryApp.Entities.ReadingLog", b =>
                {
                    b.HasOne("LibraryApp.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LibraryApp.Entities.ReplyTo", b =>
                {
                    b.HasOne("LibraryApp.Entities.Comment", "Comment")
                        .WithMany()
                        .HasForeignKey("CommentId");

                    b.HasOne("LibraryApp.Entities.Comment", "Response")
                        .WithMany()
                        .HasForeignKey("ResponseId");

                    b.Navigation("Comment");

                    b.Navigation("Response");
                });

            modelBuilder.Entity("LibraryApp.Entities.User", b =>
                {
                    b.HasOne("LibraryApp.Entities.LibraryCard", "LibraryCard")
                        .WithOne()
                        .HasForeignKey("LibraryApp.Entities.User", "LibraryCardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LibraryCard");
                });

            modelBuilder.Entity("LibraryApp.Entities.Asset", b =>
                {
                    b.Navigation("AssetTags");

                    b.Navigation("Bookmarks");
                });

            modelBuilder.Entity("LibraryApp.Entities.Assets.Tags.Tag", b =>
                {
                    b.Navigation("AssetTags");
                });

            modelBuilder.Entity("LibraryApp.Entities.AvailabilityStatus", b =>
                {
                    b.Navigation("Assets");
                });

            modelBuilder.Entity("LibraryApp.Entities.LibraryCard", b =>
                {
                    b.Navigation("Checkouts");
                });

            modelBuilder.Entity("LibraryApp.Entities.User", b =>
                {
                    b.Navigation("Bookmarks");
                });
#pragma warning restore 612, 618
        }
    }
}

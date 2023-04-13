﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SBEU.Gramm.DataLayer.DataBase;

#nullable disable

namespace SBEU.Gramm.DataLayer.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    [Migration("20220727130451_againlike4")]
    partial class againlike4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("SBEU.Gramm.DataLayer.DataBase.Entities.NCommentary", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("AuthorId")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("PostId")
                        .HasColumnType("text");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("PostId");

                    b.ToTable("Commentaries");
                });

            modelBuilder.Entity("SBEU.Gramm.DataLayer.DataBase.Entities.NContent", b =>
                {
                    b.Property<string>("ContentId")
                        .HasColumnType("text");

                    b.Property<string>("PostId")
                        .HasColumnType("text");

                    b.HasKey("ContentId", "PostId");

                    b.HasIndex("PostId");

                    b.ToTable("Contents");
                });

            modelBuilder.Entity("SBEU.Gramm.DataLayer.DataBase.Entities.NContentObject", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ImageLink")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("PhysicalPath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ContentObjects");
                });

            modelBuilder.Entity("SBEU.Gramm.DataLayer.DataBase.Entities.NFollow", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("FollowerId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FollowingId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("FollowerId");

                    b.HasIndex("FollowingId");

                    b.ToTable("Follows");
                });

            modelBuilder.Entity("SBEU.Gramm.DataLayer.DataBase.Entities.NLike", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("AuthorId")
                        .HasColumnType("text");

                    b.Property<string>("CommentaryId")
                        .HasColumnType("text");

                    b.Property<string>("ContentId")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("PostId")
                        .HasColumnType("text");

                    b.Property<string>("StoryId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("CommentaryId");

                    b.HasIndex("ContentId");

                    b.HasIndex("PostId");

                    b.HasIndex("StoryId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("SBEU.Gramm.DataLayer.DataBase.Entities.NPost", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("AuthorId")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("SBEU.Gramm.DataLayer.DataBase.Entities.NStory", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("AuthorId")
                        .HasColumnType("text");

                    b.Property<string>("ContentId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ContentId");

                    b.ToTable("Stories");
                });

            modelBuilder.Entity("SBEU.Gramm.DataLayer.DataBase.Entities.NTaggedUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("NPostId")
                        .HasColumnType("text");

                    b.Property<string>("NStoryId")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("NPostId");

                    b.HasIndex("NStoryId");

                    b.HasIndex("UserId");

                    b.ToTable("TagUsers");
                });

            modelBuilder.Entity("SBEU.Gramm.DataLayer.DataBase.Entities.NWatchedUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("NStoryId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("NStoryId");

                    b.HasIndex("UserId");

                    b.ToTable("WatchedUsers");
                });

            modelBuilder.Entity("SBEU.Gramm.DataLayer.DataBase.Entities.PostTags", b =>
                {
                    b.Property<string>("PostsId")
                        .HasColumnType("text");

                    b.Property<decimal>("TagsId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<string>("PostId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("TagId")
                        .HasColumnType("numeric(20,0)");

                    b.HasKey("PostsId", "TagsId");

                    b.HasIndex("PostId");

                    b.HasIndex("TagId");

                    b.HasIndex("TagsId");

                    b.ToTable("PostTags");
                });

            modelBuilder.Entity("SBEU.Gramm.DataLayer.DataBase.Entities.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsRevoked")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("boolean");

                    b.Property<string>("JwtId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("SBEU.Gramm.DataLayer.DataBase.Entities.Tags", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric(20,0)");

                    b.Property<string>("Tag")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("SBEU.Gramm.DataLayer.DataBase.Entities.XIdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("ImageId")
                        .HasColumnType("text");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<string>("Telegram")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("SBEU.Gramm.DataLayer.DataBase.Entities.XIdentityUserConfirm", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("UserConfirmations");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("SBEU.Gramm.DataLayer.DataBase.Entities.XIdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("SBEU.Gramm.DataLayer.DataBase.Entities.XIdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SBEU.Gramm.DataLayer.DataBase.Entities.XIdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("SBEU.Gramm.DataLayer.DataBase.Entities.XIdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SBEU.Gramm.DataLayer.DataBase.Entities.NCommentary", b =>
                {
                    b.HasOne("SBEU.Gramm.DataLayer.DataBase.Entities.XIdentityUser", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");

                    b.HasOne("SBEU.Gramm.DataLayer.DataBase.Entities.NPost", "Post")
                        .WithMany("Commentaries")
                        .HasForeignKey("PostId");

                    b.Navigation("Author");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("SBEU.Gramm.DataLayer.DataBase.Entities.NContent", b =>
                {
                    b.HasOne("SBEU.Gramm.DataLayer.DataBase.Entities.NContentObject", "Content")
                        .WithMany()
                        .HasForeignKey("ContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SBEU.Gramm.DataLayer.DataBase.Entities.NPost", "Post")
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Content");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("SBEU.Gramm.DataLayer.DataBase.Entities.NFollow", b =>
                {
                    b.HasOne("SBEU.Gramm.DataLayer.DataBase.Entities.XIdentityUser", "Follower")
                        .WithMany("Following")
                        .HasForeignKey("FollowerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SBEU.Gramm.DataLayer.DataBase.Entities.XIdentityUser", "Following")
                        .WithMany("Followers")
                        .HasForeignKey("FollowingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Follower");

                    b.Navigation("Following");
                });

            modelBuilder.Entity("SBEU.Gramm.DataLayer.DataBase.Entities.NLike", b =>
                {
                    b.HasOne("SBEU.Gramm.DataLayer.DataBase.Entities.XIdentityUser", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");

                    b.HasOne("SBEU.Gramm.DataLayer.DataBase.Entities.NCommentary", "Commentary")
                        .WithMany("Likes")
                        .HasForeignKey("CommentaryId");

                    b.HasOne("SBEU.Gramm.DataLayer.DataBase.Entities.NContentObject", "Content")
                        .WithMany("Likes")
                        .HasForeignKey("ContentId");

                    b.HasOne("SBEU.Gramm.DataLayer.DataBase.Entities.NPost", "Post")
                        .WithMany("Likes")
                        .HasForeignKey("PostId");

                    b.HasOne("SBEU.Gramm.DataLayer.DataBase.Entities.NStory", "Story")
                        .WithMany("Likes")
                        .HasForeignKey("StoryId");

                    b.Navigation("Author");

                    b.Navigation("Commentary");

                    b.Navigation("Content");

                    b.Navigation("Post");

                    b.Navigation("Story");
                });

            modelBuilder.Entity("SBEU.Gramm.DataLayer.DataBase.Entities.NPost", b =>
                {
                    b.HasOne("SBEU.Gramm.DataLayer.DataBase.Entities.XIdentityUser", "Author")
                        .WithMany("Posts")
                        .HasForeignKey("AuthorId");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("SBEU.Gramm.DataLayer.DataBase.Entities.NStory", b =>
                {
                    b.HasOne("SBEU.Gramm.DataLayer.DataBase.Entities.XIdentityUser", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");

                    b.HasOne("SBEU.Gramm.DataLayer.DataBase.Entities.NContentObject", "Content")
                        .WithMany()
                        .HasForeignKey("ContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Content");
                });

            modelBuilder.Entity("SBEU.Gramm.DataLayer.DataBase.Entities.NTaggedUser", b =>
                {
                    b.HasOne("SBEU.Gramm.DataLayer.DataBase.Entities.NPost", null)
                        .WithMany("TaggedUsers")
                        .HasForeignKey("NPostId");

                    b.HasOne("SBEU.Gramm.DataLayer.DataBase.Entities.NStory", null)
                        .WithMany("TaggedUsers")
                        .HasForeignKey("NStoryId");

                    b.HasOne("SBEU.Gramm.DataLayer.DataBase.Entities.XIdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SBEU.Gramm.DataLayer.DataBase.Entities.NWatchedUser", b =>
                {
                    b.HasOne("SBEU.Gramm.DataLayer.DataBase.Entities.NStory", "NStory")
                        .WithMany("WatchedUsers")
                        .HasForeignKey("NStoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SBEU.Gramm.DataLayer.DataBase.Entities.XIdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NStory");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SBEU.Gramm.DataLayer.DataBase.Entities.PostTags", b =>
                {
                    b.HasOne("SBEU.Gramm.DataLayer.DataBase.Entities.NPost", "Post")
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SBEU.Gramm.DataLayer.DataBase.Entities.NPost", null)
                        .WithMany()
                        .HasForeignKey("PostsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SBEU.Gramm.DataLayer.DataBase.Entities.Tags", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SBEU.Gramm.DataLayer.DataBase.Entities.Tags", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("SBEU.Gramm.DataLayer.DataBase.Entities.RefreshToken", b =>
                {
                    b.HasOne("SBEU.Gramm.DataLayer.DataBase.Entities.XIdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SBEU.Gramm.DataLayer.DataBase.Entities.XIdentityUser", b =>
                {
                    b.HasOne("SBEU.Gramm.DataLayer.DataBase.Entities.NContentObject", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId");

                    b.Navigation("Image");
                });

            modelBuilder.Entity("SBEU.Gramm.DataLayer.DataBase.Entities.NCommentary", b =>
                {
                    b.Navigation("Likes");
                });

            modelBuilder.Entity("SBEU.Gramm.DataLayer.DataBase.Entities.NContentObject", b =>
                {
                    b.Navigation("Likes");
                });

            modelBuilder.Entity("SBEU.Gramm.DataLayer.DataBase.Entities.NPost", b =>
                {
                    b.Navigation("Commentaries");

                    b.Navigation("Likes");

                    b.Navigation("TaggedUsers");
                });

            modelBuilder.Entity("SBEU.Gramm.DataLayer.DataBase.Entities.NStory", b =>
                {
                    b.Navigation("Likes");

                    b.Navigation("TaggedUsers");

                    b.Navigation("WatchedUsers");
                });

            modelBuilder.Entity("SBEU.Gramm.DataLayer.DataBase.Entities.XIdentityUser", b =>
                {
                    b.Navigation("Followers");

                    b.Navigation("Following");

                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}

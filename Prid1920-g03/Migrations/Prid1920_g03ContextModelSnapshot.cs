﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Prid1920_g03.Models;

namespace Prid1920_g03.Migrations
{
    [DbContext(typeof(Prid1920_g03Context))]
    partial class Prid1920_g03ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Prid1920_g03.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body")
                        .IsRequired();

                    b.Property<int?>("PostId");

                    b.Property<DateTime>("Timestamp");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Prid1920_g03.Models.Follow", b =>
                {
                    b.Property<int>("FollowerPseudo");

                    b.Property<int>("FolloweePseudo");

                    b.HasKey("FollowerPseudo", "FolloweePseudo");

                    b.HasIndex("FolloweePseudo");

                    b.ToTable("Follows");
                });

            modelBuilder.Entity("Prid1920_g03.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body")
                        .IsRequired();

                    b.Property<int?>("PostId");

                    b.Property<DateTime>("Timestamp");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");

                    b.HasData(
                        new
                        {
                            Id = 4,
                            Body = "Hi, why do you doing",
                            Timestamp = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "PHP"
                        },
                        new
                        {
                            Id = 3,
                            Body = "hi, please help me",
                            Timestamp = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "JAVA"
                        },
                        new
                        {
                            Id = 1,
                            Body = "what do you ask to me",
                            Timestamp = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "CSHARP"
                        },
                        new
                        {
                            Id = 2,
                            Body = "time break",
                            Timestamp = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "DOTNET"
                        });
                });

            modelBuilder.Entity("Prid1920_g03.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("BirthDate");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("PicturePath");

                    b.Property<string>("Pseudo")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<int>("Reputation");

                    b.Property<int>("Role");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Pseudo")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 4,
                            Email = "merveil@test.com",
                            FirstName = "merveil Nzitusu",
                            Password = "bruno",
                            Pseudo = "merveil",
                            Reputation = 0,
                            Role = 2
                        },
                        new
                        {
                            Id = 3,
                            Email = "iglesias@test.com",
                            FirstName = "iglesias Chendjou",
                            Password = "iglesias",
                            Pseudo = "iglesias",
                            Reputation = 0,
                            Role = 2
                        },
                        new
                        {
                            Id = 1,
                            Email = "ben@test.com",
                            FirstName = "Benoît Penelle",
                            Password = "ben",
                            Pseudo = "ben",
                            Reputation = 0,
                            Role = 1
                        },
                        new
                        {
                            Id = 2,
                            Email = "bruno@test.com",
                            FirstName = "Bruno Lacroix",
                            Password = "bruno",
                            Pseudo = "bruno",
                            Reputation = 0,
                            Role = 0
                        });
                });

            modelBuilder.Entity("Prid1920_g03.Models.Vote", b =>
                {
                    b.Property<int>("PostId");

                    b.Property<int>("UserId");

                    b.Property<DateTime>("Timestamp");

                    b.Property<int>("UpDown");

                    b.HasKey("PostId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("Votes");
                });

            modelBuilder.Entity("Prid1920_g03.Models.Comment", b =>
                {
                    b.HasOne("Prid1920_g03.Models.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId");

                    b.HasOne("Prid1920_g03.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Prid1920_g03.Models.Follow", b =>
                {
                    b.HasOne("Prid1920_g03.Models.User", "Followee")
                        .WithMany("FollowersFollows")
                        .HasForeignKey("FolloweePseudo")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Prid1920_g03.Models.User", "Follower")
                        .WithMany("FolloweesFollows")
                        .HasForeignKey("FollowerPseudo")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Prid1920_g03.Models.Post", b =>
                {
                    b.HasOne("Prid1920_g03.Models.Post")
                        .WithMany("Posts")
                        .HasForeignKey("PostId");

                    b.HasOne("Prid1920_g03.Models.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Prid1920_g03.Models.Vote", b =>
                {
                    b.HasOne("Prid1920_g03.Models.Post", "Post")
                        .WithMany("Votes")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Prid1920_g03.Models.User", "User")
                        .WithMany("Votes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Prid1920_g03.Models;

namespace Prid1920_g03.Migrations
{
    [DbContext(typeof(Prid1920_g03Context))]
    [Migration("20200109093144_data")]
    partial class data
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Prid1920_g03.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AuthorId");

                    b.Property<string>("Body")
                        .IsRequired();

                    b.Property<int>("PostId");

                    b.Property<DateTime>("Timestamp");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("PostId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Prid1920_g03.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AcceptedAnswerId");

                    b.Property<int>("AuthorId");

                    b.Property<string>("Body");

                    b.Property<int?>("ParentId");

                    b.Property<DateTime>("Timestamp");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("AcceptedAnswerId");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ParentId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Prid1920_g03.Models.PostTag", b =>
                {
                    b.Property<int>("PostId");

                    b.Property<int>("TagId");

                    b.HasKey("PostId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("PostTags");
                });

            modelBuilder.Entity("Prid1920_g03.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Prid1920_g03.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("BirthDate");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("PicturePath");

                    b.Property<string>("Pseudo")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("RefreshToken");

                    b.Property<int>("Reputation");

                    b.Property<int>("Role");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Pseudo")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Prid1920_g03.Models.Vote", b =>
                {
                    b.Property<int>("PostId");

                    b.Property<int>("AuthorId");

                    b.Property<int>("UpDown");

                    b.HasKey("PostId", "AuthorId");

                    b.HasIndex("AuthorId");

                    b.ToTable("Votes");
                });

            modelBuilder.Entity("Prid1920_g03.Models.Comment", b =>
                {
                    b.HasOne("Prid1920_g03.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Prid1920_g03.Models.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Prid1920_g03.Models.Post", b =>
                {
                    b.HasOne("Prid1920_g03.Models.Post", "AcceptedAnswer")
                        .WithMany()
                        .HasForeignKey("AcceptedAnswerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Prid1920_g03.Models.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Prid1920_g03.Models.Post", "PostParent")
                        .WithMany("Responses")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Prid1920_g03.Models.PostTag", b =>
                {
                    b.HasOne("Prid1920_g03.Models.Post", "Post")
                        .WithMany("PostTags")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Prid1920_g03.Models.Tag", "Tag")
                        .WithMany("PostTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Prid1920_g03.Models.Vote", b =>
                {
                    b.HasOne("Prid1920_g03.Models.User", "User")
                        .WithMany("Votes")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Prid1920_g03.Models.Post", "Post")
                        .WithMany("Votes")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}

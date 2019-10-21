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
<<<<<<< HEAD
                            Id = 1,
                            Email = "ben@test.com",
                            FirstName = "Benoît Penelle",
                            Password = "ben",
                            Pseudo = "ben",
                            Reputation = 0,
                            Role = 2
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
=======
                            Id = 4,
                            Email = "merveil@test.com",
                            FirstName = "merveil Nzitusu",
                            Password = "bruno",
                            Pseudo = "merveil",
                            Reputation = 0,
                            Role = 2
>>>>>>> 3d5a796dd40e06f59042346cf69a95a90e877286
                        },
                        new
                        {
                            Id = 3,
                            Email = "iglesias@test.com",
                            FirstName = "iglesias Chendjou",
                            Password = "iglesias",
                            Pseudo = "iglesias",
                            Reputation = 0,
<<<<<<< HEAD
                            Role = 0
                        },
                        new
                        {
                            Id = 4,
                            Email = "merveil@test.com",
                            FirstName = "merveil Nzitusu",
                            Password = "bruno",
                            Pseudo = "merveil",
=======
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
>>>>>>> 3d5a796dd40e06f59042346cf69a95a90e877286
                            Reputation = 0,
                            Role = 0
                        });
                });
#pragma warning restore 612, 618
        }
    }
}

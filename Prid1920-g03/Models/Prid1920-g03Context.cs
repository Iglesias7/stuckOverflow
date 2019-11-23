
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Prid1920_g03.Models
{
    public class Prid1920_g03Context : DbContext
    {
        
        public Prid1920_g03Context(DbContextOptions<Prid1920_g03Context> options): base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasIndex(u => u.Pseudo)
            .IsUnique(true);

            modelBuilder.Entity<User>().HasIndex(u => u.Email)
            .IsUnique(true);

            modelBuilder.Entity<Vote>().HasKey(v => new {v.PostId, v.UserId});
            

            modelBuilder.Entity<Follow>().HasKey(f => new { f.FollowerPseudo, f.FolloweePseudo });

            modelBuilder.Entity<Follow>()
                .HasOne<User>(f => f.Follower)
                .WithMany(m => m.FolloweesFollows)
                .HasForeignKey(f => f.FollowerPseudo)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Follow>()
                .HasOne<User>(f => f.Followee)
                .WithMany(m => m.FollowersFollows)
                .HasForeignKey(f => f.FolloweePseudo)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>().HasData(
                new User() { Id=4, Pseudo = "merveil", Password = "bruno", FirstName = "merveil Nzitusu", Email="merveil@test.com", Role = Role.Admin },
                new User() { Id=3, Pseudo = "iglesias", Password = "iglesias", FirstName = "iglesias Chendjou", Email="iglesias@test.com", Role = Role.Admin },
                new User() { Id=1, Pseudo = "ben", Password = "ben", FirstName = "Benoît Penelle", Email="ben@test.com", Role = Role.Manager },
                new User() { Id=2, Pseudo = "bruno", Password = "bruno", FirstName = "Bruno Lacroix", Email="bruno@test.com"}
            );

            modelBuilder.Entity<Post>().HasData(
                new Post() { Id=4, Title = "PHP", Body = "Hi, why do you doing"},
                new Post() { Id=3, Title = "JAVA", Body = "hi, please help me"},
                new Post() { Id=1, Title = "CSHARP", Body = "what do you ask to me"},
                new Post() { Id=2, Title = "DOTNET", Body = "time break"}
            );

            // modelBuilder.Entity<Comment>().HasData(
            //     new Comment() { Id=4, Pseudo = "merveil", Password = "bruno", FirstName = "merveil Nzitusu", Email="merveil@test.com", Role = Role.Admin },
            //     new Comment() { Id=3, Pseudo = "iglesias", Password = "iglesias", FirstName = "iglesias Chendjou", Email="iglesias@test.com", Role = Role.Admin },
            //     new Comment() { Id=1, Pseudo = "ben", Password = "ben", FirstName = "Benoît Penelle", Email="ben@test.com", Role = Role.Manager },
            //     new Comment() { Id=2, Pseudo = "bruno", Password = "bruno", FirstName = "Bruno Lacroix", Email="bruno@test.com"}
            // );

            // modelBuilder.Entity<Vote>().HasData(
            //     new Vote() { Id=4, Pseudo = "merveil", Password = "bruno", FirstName = "merveil Nzitusu", Email="merveil@test.com", Role = Role.Admin },
            //     new Vote() { Id=3, Pseudo = "iglesias", Password = "iglesias", FirstName = "iglesias Chendjou", Email="iglesias@test.com", Role = Role.Admin },
            //     new Vote() { Id=1, Pseudo = "ben", Password = "ben", FirstName = "Benoît Penelle", Email="ben@test.com", Role = Role.Manager },
            //     new Vote() { Id=2, Pseudo = "bruno", Password = "bruno", FirstName = "Bruno Lacroix", Email="bruno@test.com"}
            // );

            // modelBuilder.Entity<Tag>().HasData(
            //     new Tag() { Id=4, Pseudo = "merveil", Password = "bruno", FirstName = "merveil Nzitusu", Email="merveil@test.com", Role = Role.Admin },
            //     new Tag() { Id=3, Pseudo = "iglesias", Password = "iglesias", FirstName = "iglesias Chendjou", Email="iglesias@test.com", Role = Role.Admin },
            //     new Tag() { Id=1, Pseudo = "ben", Password = "ben", FirstName = "Benoît Penelle", Email="ben@test.com", Role = Role.Manager },
            //     new Tag() { Id=2, Pseudo = "bruno", Password = "bruno", FirstName = "Bruno Lacroix", Email="bruno@test.com"}
            // );
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Follow> Follows { get; set; }
        // public DbSet<Tag> Tags { get; set; }        
    }
}
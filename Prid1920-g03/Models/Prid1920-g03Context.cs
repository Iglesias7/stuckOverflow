
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

            modelBuilder.Entity<Vote>().HasKey(v => new {v.PostId, v.AuthorId});
            

            modelBuilder.Entity<Follow>().HasKey(f => new { f.FollowerPseudo, f.FolloweePseudo });
            modelBuilder.Entity<PostTag>().HasKey(pt => new { pt.PostId, pt.TagId });

            modelBuilder.Entity<Tag>().HasIndex(u => u.Name)
            .IsUnique(true);

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



            modelBuilder.Entity<PostTag>()
                .HasOne<Post>(p => p.Post)
                .WithMany(m => m.LsPostTags)
                .HasForeignKey(f => f.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PostTag>()
                .HasOne<Tag>(f => f.Tag)
                .WithMany(m => m.PostTags)
                .HasForeignKey(f => f.TagId)
                .OnDelete(DeleteBehavior.Restrict);

                base.OnModelCreating(modelBuilder);
                
                // Comment.Post (1) <--> Post.Comments (*)
                modelBuilder.Entity<Comment>()
                    .HasOne(c => c.Post)                  // définit la propriété de navigation pour le côté (1) de la relation
                    .WithMany(p => p.Comments)            // définit la propriété de navigation pour le côté (N) de la relation
                    .HasForeignKey(c => c.PostId)         // spécifie que la clé étrangère est Comment.PostId
                    .OnDelete(DeleteBehavior.Restrict);   // spécifie le comportement en cas de delete : ici, un refus
          
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Tag> Tags { get; set; }  
        public DbSet<PostTag> PostTags { get; set; }        
    }
}
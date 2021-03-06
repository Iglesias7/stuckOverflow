
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

//---------------------------------------------------------------------------------------------------------------
            modelBuilder.Entity<User>().HasIndex(u => u.Pseudo).IsUnique(true);

            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique(true);

            modelBuilder.Entity<Tag>().HasIndex(u => u.Name).IsUnique(true);

//------------------------------------------------------------------------------------------------------------------

            modelBuilder.Entity<Vote>().HasKey(v => new {v.PostId, v.AuthorId});
            
            modelBuilder.Entity<PostTag>().HasKey(pt => new { pt.PostId, pt.TagId });


//----------------------------------------------------------------------------------------------------------------

                
                // Comment.Post (1) <--> Post.Comments (*)
                modelBuilder.Entity<Comment>()
                    .HasOne<Post>(c => c.Post)                  // définit la propriété de navigation pour le côté (1) de la relation
                    .WithMany(p => p.Comments)            // définit la propriété de navigation pour le côté (N) de la relation
                    .HasForeignKey(c => c.PostId)         // spécifie que la clé étrangère est Comment.PostId
                    .OnDelete(DeleteBehavior.Restrict);   // spécifie le comportement en cas de delete : ici, un refus

                // Comment.User (1) <--> User.Comments (*)
                modelBuilder.Entity<Comment>()
                    .HasOne<User>(c => c.User)                  
                    .WithMany(p => p.Comments)            
                    .HasForeignKey(c => c.AuthorId)        
                    .OnDelete(DeleteBehavior.Restrict);

                // Post.User (1) <--> User.Posts (*)
                modelBuilder.Entity<Post>()
                    .HasOne<User>(c => c.User)                  
                    .WithMany(p => p.Posts)            
                    .HasForeignKey(c => c.AuthorId)        
                    .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<Post>()
                    .HasOne<Post>(f => f.PostParent)
                    .WithMany(m => m.Responses)
                    .HasForeignKey(f => f.ParentId)
                    .OnDelete(DeleteBehavior.Restrict);
                    
                modelBuilder.Entity<Post>()
                    .HasOne<Post>(f => f.AcceptedAnswer)
                    .WithMany()
                    .HasForeignKey(f => f.AcceptedAnswerId)
                    .OnDelete(DeleteBehavior.Restrict);
                    
                // Vote.User (1) <--> User.Votes (*)
                modelBuilder.Entity<Vote>()
                    .HasOne<User>(c => c.User)                  
                    .WithMany(p => p.Votes)            
                    .HasForeignKey(c => c.AuthorId)        
                    .OnDelete(DeleteBehavior.Restrict);

                // Vote.Post (1) <--> Post.Votes (*)
                modelBuilder.Entity<Vote>()
                    .HasOne<Post>(c => c.Post)                  
                    .WithMany(p => p.Votes)            
                    .HasForeignKey(c => c.PostId)        
                    .OnDelete(DeleteBehavior.Restrict);

                // PostTag.Post (1) <--> Post.LsPostTags (*)
                modelBuilder.Entity<PostTag>()
                    .HasOne<Post>(p => p.Post)
                    .WithMany(m => m.PostTags)
                    .HasForeignKey(f => f.PostId)
                    .OnDelete(DeleteBehavior.Restrict);
                
                // PostTag.Tag (1) <--> Tag.LsPostTags (*)
                modelBuilder.Entity<PostTag>()
                    .HasOne<Tag>(f => f.Tag)
                    .WithMany(m => m.PostTags)
                    .HasForeignKey(f => f.TagId)
                    .OnDelete(DeleteBehavior.Restrict);

          
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Tag> Tags { get; set; }  
        public DbSet<PostTag> PostTags { get; set; }        
    }
}
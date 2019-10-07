
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Prid1920_g03.Models
{
    public class Prid1920_g03Context : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Properties configurations

            modelBuilder.Entity<User>()
            .Property(u => u.Id)
             .ValueGeneratedOnAdd();

            modelBuilder.Entity<User>().HasIndex(u => u.Pseudo)
            .IsUnique(true);
            
            modelBuilder.Entity<User>().HasIndex(u => u.Email)
            .IsUnique(true);
        }

        public Prid1920_g03Context(DbContextOptions<Prid1920_g03Context> options)
            : base(options)
        {
        }




    }
}
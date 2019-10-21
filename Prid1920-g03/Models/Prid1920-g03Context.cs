
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

            modelBuilder.Entity<User>().HasIndex(u => u.Pseudo)
            .IsUnique(true);

            modelBuilder.Entity<User>().HasIndex(u => u.Email)
            .IsUnique(true);

            modelBuilder.Entity<User>().HasData(
                new User() { Id=4, Pseudo = "merveil", Password = "merveil", FirstName = "merveil Nzitusu", Email="merveil@test.com", Role = Role.Admin },
                new User() { Id=3, Pseudo = "iglesias", Password = "iglesias", FirstName = "iglesias Chendjou", Email="iglesias@test.com", Role = Role.Admin },
                new User() { Id=1, Pseudo = "ben", Password = "ben", FirstName = "Benoît Penelle", Email="ben@test.com", Role = Role.Manager },
                new User() { Id=2, Pseudo = "bruno", Password = "bruno", FirstName = "Bruno Lacroix", Email="bruno@test.com", Role = Role.Member }
                
                
            );

        }

        public Prid1920_g03Context(DbContextOptions<Prid1920_g03Context> options)
            : base(options)
        {
        }




    }
}
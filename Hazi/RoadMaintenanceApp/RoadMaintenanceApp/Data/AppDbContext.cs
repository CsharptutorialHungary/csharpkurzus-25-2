using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RoadMaintenanceApp.Models;

namespace RoadMaintenanceApp.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Street> Streets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=road_maintenance.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Street>()
                .HasIndex(s => s.Id)
                .IsUnique();
        }
    }
}

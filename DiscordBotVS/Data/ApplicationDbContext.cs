using Features.lib.Levels;
using Features.lib.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBotVS.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Level> Levels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=app.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // One to One User <=> Level
            modelBuilder.Entity<User>()
                .HasOne(u => u.UserLevel)
                .WithOne(l => l.User)
                .HasForeignKey<Level>(l => l.UserId);
        }

        
    }
}

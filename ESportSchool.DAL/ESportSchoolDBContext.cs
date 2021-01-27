using System;
using ESportSchool.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;

namespace ESportSchool.DAL
{
    public sealed class ESportSchoolDBContext : DbContext
    {
        public DbSet<CoachProfile> CoachProfiles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Guide> Guides { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<News> Newses { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ScheduleInterval> ScheduleIntervals { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }

        public ESportSchoolDBContext()
        {
            // Database.EnsureDeleted();    
            Database.EnsureCreated();    
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=localhost;UId=root;Port=3306;Database=esportschool;pwd=rootsergeevich", new MySqlServerVersion(new Version(8,0,20))); //TODO move to config
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}

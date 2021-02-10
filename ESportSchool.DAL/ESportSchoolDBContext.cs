using System;
using ESportSchool.Domain.Entities;
using ESportSchool.Domain.Entities.Mapped;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql;

namespace ESportSchool.DAL
{
    public sealed class ESportSchoolDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        
        public DbSet<Coach> CoachProfiles { get; set; }
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

        public ESportSchoolDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            // Database.EnsureDeleted();    
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("PostgreSQL"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}

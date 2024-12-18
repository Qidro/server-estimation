﻿using Microsoft.EntityFrameworkCore;
using server_estimation.Models;

namespace server_estimation.DataAccess
{
    public class EstimationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public EstimationDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Survey> Survey { get; set; }

        public DbSet<Question> Question { get; set; }

        public DbSet<Answers> Answers { get; set; }

        public DbSet<SurveyResults> SurveyResults { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Database"));
        }
    }
}

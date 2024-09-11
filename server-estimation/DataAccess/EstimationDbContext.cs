using Microsoft.EntityFrameworkCore;
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

        public DbSet<Users> User => Set<Users>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Database"));
        }
    }
}

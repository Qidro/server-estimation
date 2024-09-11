using Microsoft.EntityFrameworkCore;

namespace server_estimation.DataAccess
{
    public class EstimationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public EstimationDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Database"));
        }
    }
}

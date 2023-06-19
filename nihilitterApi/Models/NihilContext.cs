using Microsoft.EntityFrameworkCore;

namespace NihilitterApi.Models
{
    public class NihilContext : DbContext
    {
        private readonly IConfiguration configuration;

        public NihilContext(DbContextOptions<NihilContext> options, IConfiguration configuration)
           : base(options)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString: this.configuration.GetConnectionString("NihilItemsConnection"));
        }

        public DbSet<Nihil> NihilItems { get; set; } = null!;
    }
}
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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>()
                            .HasOne(m => m.From)
                            .WithMany()
                            .HasForeignKey(m => m.FromId);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.To)
                .WithMany()
                .HasForeignKey(m => m.ToId);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString: this.configuration.GetConnectionString("NihilItemsConnection"));
        }

        public DbSet<Nihil> NihilItems { get; set; } = null!;
        public DbSet<User> ApplicationUsers { get; set; } = null!;
        public DbSet<Message> Messages { get; set; } = null!;
    }
}
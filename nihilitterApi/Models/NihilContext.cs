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
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .HasMaxLength(100);

                entity.Property(e => e.Country)
                    .HasMaxLength(100);

                entity.Property(e => e.Email)
                    .HasMaxLength(100);

                entity.Property(e => e.Password)
                    .HasMaxLength(100);

                entity.HasMany(e => e.Posts)
                    .WithOne(e => e.User)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Friends)
                    .WithOne()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Nihil>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Post);

                entity.Property(e => e.PostDate);

                entity.HasOne(e => e.User)
                    .WithMany(e => e.Posts)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Friend>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.UserId);

                entity.Property(e => e.FriendId);

                entity.Property(e => e.isConfirmed);
            });

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString: this.configuration.GetConnectionString("NihilItemsConnection"));
        }

        public DbSet<Nihil> NihilItems { get; set; } = null!;
        public DbSet<User> ApplicationUsers { get; set; } = null!;
        public DbSet<Message> Messages { get; set; } = null!;
        public DbSet<Friend> Friends { get; set; } = null!;
    }
}
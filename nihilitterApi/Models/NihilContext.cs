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
    modelBuilder.Entity<Message>(entity =>
    {
        entity.HasKey(m => m.Id);
        entity.Property(m => m.MessageBody).IsRequired();

        entity.HasOne(m => m.From)
            .WithMany()
            .HasForeignKey(m => m.FromId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(m => m.To)
            .WithMany()
            .HasForeignKey(m => m.ToId)
            .OnDelete(DeleteBehavior.Restrict);
    });

    modelBuilder.Entity<Nihil>(entity =>
    {
        entity.HasKey(n => n.Id);
        entity.Property(n => n.Post).IsRequired();
        entity.Property(n => n.PostDate).IsRequired();

        entity.HasOne(n => n.User)
            .WithMany(u => u.Posts)
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    });

    modelBuilder.Entity<User>(entity =>
    {
        entity.HasKey(u => u.Id);
        entity.Property(u => u.FirstName).IsRequired();
        entity.Property(u => u.LastName).IsRequired();
        entity.Property(u => u.Country);
        entity.Property(u => u.Email);
        entity.Property(u => u.Password);

        entity.HasMany(u => u.Friends)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "UserFriend",
                u => u.HasOne<User>().WithMany().HasForeignKey("UserId"),
                f => f.HasOne<User>().WithMany().HasForeignKey("FriendId"),
                j => j.HasKey("UserId", "FriendId")
            );

        entity.HasMany(u => u.Messages)
            .WithOne()
            .HasForeignKey(m => m.FromId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasMany(u => u.Messages)
            .WithOne()
            .HasForeignKey(m => m.ToId)
            .OnDelete(DeleteBehavior.Restrict);
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
    }
}
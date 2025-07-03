using Microsoft.EntityFrameworkCore;
namespace BeToff.Entities;

public class BeToffDbContext : DbContext
{
    public BeToffDbContext(DbContextOptions<BeToffDbContext> options) : base(options) { }

    public string ConnectionString { get; set; }
    protected BeToffDbContext(string connectionString) {
        ConnectionString = connectionString;
    }

    public DbSet<User> Users {get; set;}
    public DbSet<Familly> Famillies {get; set;}
    public DbSet<Photo> Photos {get; set;}
    public DbSet<Comment> Comments {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        };

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
    }
}
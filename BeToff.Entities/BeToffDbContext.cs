using Microsoft.EntityFrameworkCore;
namespace BeToff.Entities;

public class BeToffDbContext : DbContext
{
    public BeToffDbContext(DbContextOptions<BeToffDbContext> options) : base(options) { }

    public string ConnectionString { get; set; }
    public BeToffDbContext(string connectionString) {
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
}
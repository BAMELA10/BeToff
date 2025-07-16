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

    public DbSet<Registration> Registration { get; set; }

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

        modelBuilder.Entity<User>()
            .HasMany(e => e.FamilliesCreator)
            .WithOne(e => e.CreatedBy)
            .HasForeignKey(e => e.IdCreator);

        modelBuilder.Entity<User>()
            .HasMany(e => e.FamilliesHeadof)
            .WithOne(e => e.Headof)
            .HasForeignKey(e => e.IdHead);

        modelBuilder.Entity<User>()
            .HasMany(e => e.Famillies)
            .WithMany(e => e.Members)
            .UsingEntity<Registration>(
                r => r.HasOne<Familly>(e => e.Familly).WithMany(e => e.Registrations),
                l => l.HasOne<User>(e => e.User).WithMany(e => e.Registrations)
            );

    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
    }
}
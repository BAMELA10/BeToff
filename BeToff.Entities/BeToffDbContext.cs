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
    public DbSet<Invitation> Invitations { get; set; }

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
            .OnDelete(DeleteBehavior.NoAction) // Definition NoAction for OnDelete Operation because in this System the User Will never Delete of DB
            .HasForeignKey(e => e.IdCreator);


        modelBuilder.Entity<User>()
            .HasMany(e => e.FamilliesHeadof)
            .WithOne(e => e.Headof)
            .OnDelete(DeleteBehavior.NoAction)
            .HasForeignKey(e => e.IdHead);
            

        modelBuilder.Entity<User>()
            .HasMany(e => e.Famillies)
            .WithMany(e => e.Members)
            .UsingEntity<Registration>(
                r => r.HasOne<Familly>(e => e.Familly).WithMany(e => e.Registrations),
                l => l.HasOne<User>(e => e.User).WithMany(e => e.Registrations)
            )
            ;
            

        modelBuilder.Entity<User>()
            .HasMany(e => e.InvitationsSender)
            .WithOne(e => e.Sender)
            .OnDelete(DeleteBehavior.NoAction)
            .HasForeignKey(e => e.SenderId);


        modelBuilder.Entity<User>()
            .HasMany(e => e.InvitationsReceiver)
            .WithOne(e => e.Receiver)
            .OnDelete(DeleteBehavior.NoAction)
            .HasForeignKey(e => e.ReceiverId);
            

        modelBuilder.Entity<Familly>()
            .HasMany(e => e.Invitations)
            .WithOne(e => e.FamillyItem)
            .HasForeignKey(e => e.FamillyId);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
    }
}
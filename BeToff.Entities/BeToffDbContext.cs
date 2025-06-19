namespace BeToff.Entities;

public class BeToffDbContext : DbContext
{
    public DbSet<User> Users {get; set;}
    public DbSet<Familly> Famillies {get; set;}
    public DbSet<Photo> Photos {get; set;}
    public DbSet<Comment> Comments {get; set;}
}
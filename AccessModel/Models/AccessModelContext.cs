using Microsoft.EntityFrameworkCore;

namespace AccessModel.Models;

public sealed class AccessModelContext : DbContext
{       
    public DbSet<User>? Users { get; set; }
    public DbSet<Resource>? Resources { get; set; }
    public DbSet<Permissions>? Permissions { get; set; }
    public DbSet<AccessControlEntry>? UsersAccessControlEntries { get; set; }
    private const string ConnectionString = "Host=127.0.0.1; Port=5432; Database=AccessModel; Username=root; Password=123456789;";
    
    public AccessModelContext() { Database.EnsureCreated(); }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(ConnectionString);
        base.OnConfiguring(optionsBuilder);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}
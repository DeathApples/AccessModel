using Microsoft.EntityFrameworkCore;

namespace AccessModel.Models;

public sealed class AccessModelContext : DbContext
{       
    public DbSet<User> Users { get; set; }
    public DbSet<Resource> Resources { get; set; }
    public DbSet<DeletionRequest> Requests { get; set; }
    private const string ConnectionString = "Host=127.0.0.1; Port=5432; Database=MandatoryAccessModel; Username=student; Password=A123!;";
    
    public AccessModelContext() { Database.EnsureCreated(); }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(ConnectionString);
        base.OnConfiguring(optionsBuilder);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccessModel.Models;
using Microsoft.EntityFrameworkCore;
namespace AccessModel.Services;

class UserContext : DbContext
{       
    public DbSet<User> Users { get; set; }
    public DbSet<Role> UserRoles { get; set; }
    public DbSet<Resource> Resources { get; set; }
    public DbSet<Permission> UsersPermissions { get; set; }
    public DbSet<AccessControlEntry> UsersAccessControlEntries { get; set; }
    static readonly string connectionString = "Host=127.0.0.1; Port=5432 ; Database=AccessModel;Username=root; Password=123456789;";
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(connectionString);
        base.OnConfiguring(optionsBuilder);
    }
    // UserContext db = new UserContext()
}



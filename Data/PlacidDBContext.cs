 
using W1.Models;
using Microsoft.EntityFrameworkCore;
using static NuGet.Packaging.PackagingConstants;

namespace W1.Data;

public class PlacidDBContext : DbContext
{
    public PlacidDBContext(DbContextOptions<PlacidDBContext> options) : base(options )
    {

        
    }

    
    public DbSet<Member> Members { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }

public DbSet<W1.Models.UserModel> UserModel { get; set; } = default!;
}
 
using W1.Models;
using Microsoft.EntityFrameworkCore;

namespace W1.Data;

public class PlacidDBContext : DbContext
{
    public PlacidDBContext(DbContextOptions<PlacidDBContext> options) : base(options)
    {
    }
           
    public DbSet<Member> Members { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}
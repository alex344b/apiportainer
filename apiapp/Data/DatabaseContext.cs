using apiapp.Models;
using Microsoft.EntityFrameworkCore;

namespace apiapp.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
    
    public DbSet<Game> Games { get; set; }
}
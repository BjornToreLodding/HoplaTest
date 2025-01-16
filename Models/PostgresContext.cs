using Microsoft.EntityFrameworkCore;
using UsersApi.Models;

namespace UsersApi.Data; // Sørg for at dette samsvarer med prosjektets struktur

public class PostgresContext : DbContext
{
    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Horse> Horses { get; set; }
}

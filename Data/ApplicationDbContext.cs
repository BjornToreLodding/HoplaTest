using Microsoft.EntityFrameworkCore;
using UsersApi.Models;

namespace UsersApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Horse> Horses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Konfigurer relasjonen mellom Horse og User
        modelBuilder.Entity<Horse>()
            .HasOne(h => h.User)             // Navigasjon til User
            .WithMany(u => u.Horses)         // En bruker kan ha mange hester
            .HasForeignKey(h => h.Owner);    // Fremmednøkkelen heter "Owner"
    }
}

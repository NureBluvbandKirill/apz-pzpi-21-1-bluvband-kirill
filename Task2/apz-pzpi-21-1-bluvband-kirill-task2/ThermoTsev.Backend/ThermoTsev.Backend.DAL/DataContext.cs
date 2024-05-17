using Microsoft.EntityFrameworkCore;
using ThermoTsev.Backend.Domain.Entities;

namespace ThermoTsev.Backend.DAL;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; } = null!;

    public DbSet<Notification> Notifications  { get; set; } = null!;

    public DbSet<Shipment> Shipments { get; set; } = null!;

    public DbSet<ShipmentCondition> ShipmentConditions { get; set; } = null!;

    public DbSet<Analytic> Analytics { get; set; } = null!;

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Shipment>()
            .HasOne(s => s.StartLocation)
            .WithMany(l => l.StartShipments)
            .HasForeignKey(s => s.StartLocationId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Shipment>()
            .HasOne(s => s.EndLocation)
            .WithMany(l => l.EndShipments)
            .HasForeignKey(s => s.EndLocationId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<ShipmentCondition>()
            .HasOne(sc => sc.Shipment)
            .WithOne(s => s.ShipmentCondition)
            .HasForeignKey<ShipmentCondition>(sc => sc.ShipmentId);
    }
}

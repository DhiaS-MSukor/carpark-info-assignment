using CarparkInfoAssignmentDhia.CarparkInfo.Entities;
using CarparkInfoAssignmentDhia.CarparkInfo.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarparkInfoAssignmentDhia.CarparkInfo;
public class CarparkContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserFavoriteCarpark> UserFavoriteCarparks { get; set; }
    public DbSet<Carpark> Carparks { get; set; }
    public DbSet<CarParkType> CarParkTypes { get; set; }
    public DbSet<FreeParkingType> FreeParkingTypes { get; set; }
    public DbSet<ParkingSystemType> ParkingSystemTypes { get; set; }
    public DbSet<ShortTermParkingType> ShortTermParkingTypes { get; set; }

    public CarparkContext(DbContextOptions<CarparkContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserFavoriteCarparkConfiguration());
        modelBuilder.ApplyConfiguration(new CarparkConfiguration());
        modelBuilder.ApplyConfiguration(new CarParkTypeConfiguration());
        modelBuilder.ApplyConfiguration(new FreeParkingTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ParkingSystemTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ShortTermParkingTypeConfiguration());
    }
}

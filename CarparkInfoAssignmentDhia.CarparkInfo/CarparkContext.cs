using CarparkInfoAssignmentDhia.CarparkInfo.Entities;
using CarparkInfoAssignmentDhia.CarparkInfo.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarparkInfoAssignmentDhia.CarparkInfo;
public class CarparkContext : DbContext
{
    public DbSet<Carpark> Carparks { get; set; }
    public DbSet<CarParkType> CarParkTypes { get; set; }
    public DbSet<FreeParkingType> FreeParkingTypes { get; set; }
    public DbSet<ParkingSystemType> ParkingSystemTypes { get; set; }
    public DbSet<ShortTermParkingType> ShortTermParkingTypes { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CarparkTypeConfiguration());
    }
}

using CarparkInfoAssignmentDhia.CarparkInfo.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarparkInfoAssignmentDhia.CarparkInfo.EntityTypeConfigurations;
public class CarparkConfiguration : IEntityTypeConfiguration<Carpark>
{
    public void Configure(EntityTypeBuilder<Carpark> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.CarParkNo).IsUnique();

        builder.Property(x => x.Address).IsRequired();
        builder.Property(x => x.CarParkNo).IsRequired();

        builder.Property(x => x.XCoord).HasPrecision(4);
        builder.Property(x => x.YCoord).HasPrecision(4);

        builder.HasOne(x => x.CarParkType)
            .WithMany(x => x.Carparks)
            .HasPrincipalKey(x => x.Id)
            .HasForeignKey(x => x.CarParkType_Id)
            .IsRequired();

        builder.HasOne(x => x.FreeParkingType)
            .WithMany(x => x.Carparks)
            .HasPrincipalKey(x => x.Id)
            .HasForeignKey(x => x.FreeParkingType_Id)
            .IsRequired();

        builder.HasOne(x => x.ParkingSystemType)
            .WithMany(x => x.Carparks)
            .HasPrincipalKey(x => x.Id)
            .HasForeignKey(x => x.ParkingSystemType_Id)
            .IsRequired();

        builder.HasOne(x => x.ShortTermParkingType)
            .WithMany(x => x.Carparks)
            .HasPrincipalKey(x => x.Id)
            .HasForeignKey(x => x.ShortTermParkingType_Id)
            .IsRequired();

        builder.HasMany(x => x.UserFavoriteCarparks)
            .WithOne(x => x.Carpark)
            .HasPrincipalKey(x => x.Id)
            .HasForeignKey(x => x.Carpark_Id);
    }
}

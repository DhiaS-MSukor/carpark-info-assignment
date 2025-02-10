using CarparkInfoAssignmentDhia.CarparkInfo.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarparkInfoAssignmentDhia.CarparkInfo.EntityTypeConfigurations;
public class CarparkTypeConfiguration : IEntityTypeConfiguration<Carpark>
{
    public void Configure(EntityTypeBuilder<Carpark> builder)
    {
        builder.HasOne(x => x.CarParkType)
            .WithMany(x => x.Carparks)
            .HasPrincipalKey(x => x.Id)
            .HasForeignKey(x => x.CarParkType_Id);

        builder.HasOne(x => x.FreeParkingType)
            .WithMany(x => x.Carparks)
            .HasPrincipalKey(x => x.Id)
            .HasForeignKey(x => x.FreeParkingType_Id);

        builder.HasOne(x => x.ParkingSystemType)
            .WithMany(x => x.Carparks)
            .HasPrincipalKey(x => x.Id)
            .HasForeignKey(x => x.ParkingSystemType_Id);

        builder.HasOne(x => x.ShortTermParkingType)
            .WithMany(x => x.Carparks)
            .HasPrincipalKey(x => x.Id)
            .HasForeignKey(x => x.ShortTermParkingType_Id);
    }
}

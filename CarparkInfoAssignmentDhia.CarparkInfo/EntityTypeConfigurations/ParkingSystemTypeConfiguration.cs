using CarparkInfoAssignmentDhia.CarparkInfo.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarparkInfoAssignmentDhia.CarparkInfo.EntityTypeConfigurations;

public class ParkingSystemTypeConfiguration : IEntityTypeConfiguration<ParkingSystemType>
{
    public void Configure(EntityTypeBuilder<ParkingSystemType> builder)
    {
        builder.HasMany(x => x.Carparks)
            .WithOne(x => x.ParkingSystemType)
            .HasPrincipalKey(x => x.Id)
            .HasForeignKey(x => x.ParkingSystemType_Id);
    }
}

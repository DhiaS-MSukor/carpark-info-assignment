using CarparkInfoAssignmentDhia.CarparkInfo.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarparkInfoAssignmentDhia.CarparkInfo.EntityTypeConfigurations;

public class ShortTermParkingTypeConfiguration : IEntityTypeConfiguration<ShortTermParkingType>
{
    public void Configure(EntityTypeBuilder<ShortTermParkingType> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Name).IsUnique();

        builder.HasMany(x => x.Carparks)
            .WithOne(x => x.ShortTermParkingType)
            .HasPrincipalKey(x => x.Id)
            .HasForeignKey(x => x.ShortTermParkingType_Id)
            .IsRequired();
    }
}

using CarparkInfoAssignmentDhia.CarparkInfo.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarparkInfoAssignmentDhia.CarparkInfo.EntityTypeConfigurations;

public class CarParkTypeConfiguration : IEntityTypeConfiguration<CarParkType>
{
    public void Configure(EntityTypeBuilder<CarParkType> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Name).IsUnique();

        builder.HasMany(x => x.Carparks)
            .WithOne(x => x.CarParkType)
            .HasPrincipalKey(x => x.Id)
            .HasForeignKey(x => x.CarParkType_Id)
            .IsRequired();
    }
}

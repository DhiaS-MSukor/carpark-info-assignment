using CarparkInfoAssignmentDhia.CarparkInfo.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarparkInfoAssignmentDhia.CarparkInfo.EntityTypeConfigurations;

public class FreeParkingTypeConfiguration : IEntityTypeConfiguration<FreeParkingType>
{
    public void Configure(EntityTypeBuilder<FreeParkingType> builder)
    {
        builder.HasMany(x => x.Carparks)
            .WithOne(x => x.FreeParkingType)
            .HasPrincipalKey(x => x.Id)
            .HasForeignKey(x => x.FreeParkingType_Id);
    }
}

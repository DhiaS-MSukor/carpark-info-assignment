using CarparkInfoAssignmentDhia.CarparkInfo.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarparkInfoAssignmentDhia.CarparkInfo.EntityTypeConfigurations;

public class UserFavoriteCarparkConfiguration : IEntityTypeConfiguration<UserFavoriteCarpark>
{
    public void Configure(EntityTypeBuilder<UserFavoriteCarpark> builder)
    {
        builder.HasKey(x => new { x.Carpark_Id, x.User_Id });

        builder.HasOne(x => x.User)
            .WithMany(x => x.UserFavoriteCarparks)
            .HasPrincipalKey(x => x.Id)
            .HasForeignKey(x => x.User_Id)
            .IsRequired();

        builder.HasOne(x => x.Carpark)
            .WithMany(x => x.UserFavoriteCarparks)
            .HasPrincipalKey(x => x.Id)
            .HasForeignKey(x => x.Carpark_Id)
            .IsRequired();
    }
}

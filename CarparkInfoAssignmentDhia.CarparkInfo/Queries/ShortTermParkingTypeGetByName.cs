using CarparkInfoAssignmentDhia.CarparkInfo.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarparkInfoAssignmentDhia.CarparkInfo.Queries;

public class ShortTermParkingTypeGetByName
{
    private readonly string name;

    public ShortTermParkingTypeGetByName(string name)
    {
        this.name = name;
    }

    public Task<ShortTermParkingType[]> Query(DbSet<ShortTermParkingType> set, CancellationToken cancellationToken = default)
    {
        return set.Where(x => x.Name == name).ToArrayAsync(cancellationToken);
    }
}

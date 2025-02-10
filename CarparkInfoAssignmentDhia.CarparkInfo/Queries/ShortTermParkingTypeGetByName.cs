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

    public IQueryable<ShortTermParkingType> Query(DbSet<ShortTermParkingType> set)
    {
        return set.Where(x => x.Name == name);
    }
}

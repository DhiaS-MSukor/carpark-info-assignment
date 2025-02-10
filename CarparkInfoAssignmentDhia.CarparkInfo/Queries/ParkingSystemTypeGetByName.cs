using CarparkInfoAssignmentDhia.CarparkInfo.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarparkInfoAssignmentDhia.CarparkInfo.Queries;

public class ParkingSystemTypeGetByName
{
    private readonly string name;

    public ParkingSystemTypeGetByName(string name)
    {
        this.name = name;
    }

    public IQueryable<ParkingSystemType> Query(DbSet<ParkingSystemType> set)
    {
        return set.Where(x => x.Name == name);
    }
}

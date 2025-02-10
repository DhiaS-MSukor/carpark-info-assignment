using CarparkInfoAssignmentDhia.CarparkInfo.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarparkInfoAssignmentDhia.CarparkInfo.Queries;

public class FreeParkingTypeGetByName
{
    private readonly string name;

    public FreeParkingTypeGetByName(string name)
    {
        this.name = name;
    }

    public IQueryable<FreeParkingType> Query(DbSet<FreeParkingType> set)
    {
        return set.Where(x => x.Name == name);
    }
}

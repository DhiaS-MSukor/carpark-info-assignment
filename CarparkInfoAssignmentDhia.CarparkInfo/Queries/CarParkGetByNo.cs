using CarparkInfoAssignmentDhia.CarparkInfo.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarparkInfoAssignmentDhia.CarparkInfo.Queries;

public class CarparkGetByNo
{
    private readonly string number;

    public CarparkGetByNo(string number)
    {
        this.number = number;
    }

    public IQueryable<Carpark> Query(DbSet<Carpark> set)
    {
        return set.Where(x => x.CarParkNo == number);
    }
}

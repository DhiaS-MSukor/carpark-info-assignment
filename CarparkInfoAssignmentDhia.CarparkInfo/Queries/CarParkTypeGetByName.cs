using CarparkInfoAssignmentDhia.CarparkInfo.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarparkInfoAssignmentDhia.CarparkInfo.Queries;
public class CarParkTypeGetByName
{
    private readonly string name;

    public CarParkTypeGetByName(string name)
    {
        this.name = name;
    }

    public Task<CarParkType[]> Query(DbSet<CarParkType> set, CancellationToken cancellationToken = default)
    {
        return set.Where(x => x.Name == name).ToArrayAsync(cancellationToken);
    }
}

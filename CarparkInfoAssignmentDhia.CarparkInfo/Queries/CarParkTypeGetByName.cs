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

    public IQueryable<CarParkType> Query(DbSet<CarParkType> set)
    {
        return set.Where(x => x.Name == name);
    }
}

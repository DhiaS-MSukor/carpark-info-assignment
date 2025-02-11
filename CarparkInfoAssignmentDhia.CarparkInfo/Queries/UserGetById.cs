using CarparkInfoAssignmentDhia.CarparkInfo.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarparkInfoAssignmentDhia.CarparkInfo.Queries;
public class UserGetById
{
    private readonly int id;

    public UserGetById(int id)
    {
        this.id = id;
    }

    public IQueryable<User> Query(DbSet<User> set)
    {
        return set.Where(x => x.Id == id);
    }
}

using CarparkInfoAssignmentDhia.CarparkInfo.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarparkInfoAssignmentDhia.CarparkInfo.Queries;
public class UserFavoriteCarparkGetByUserIdCarparkId
{
    private readonly int userId;
    private readonly int carparkId;

    public UserFavoriteCarparkGetByUserIdCarparkId(int userId, int carparkId)
    {
        this.userId = userId;
        this.carparkId = carparkId;
    }

    public IQueryable<UserFavoriteCarpark> Query(DbSet<UserFavoriteCarpark> set)
    {
        return set.Where(x => x.Carpark_Id == carparkId && x.User_Id == userId);
    }
}

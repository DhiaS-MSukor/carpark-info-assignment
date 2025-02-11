using CarparkInfoAssignmentDhia.CarparkInfo.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarparkInfoAssignmentDhia.CarparkInfo.Queries;
public class CarparkGetWithFilters
{
    private readonly bool? freeParking;
    private readonly bool? nightParking;
    private readonly decimal? vehicleHeight;

    public CarparkGetWithFilters(
        bool? freeParking = null,
        bool? nightParking = null,
        decimal? vehicleHeight = null)
    {
        this.freeParking = freeParking;
        this.nightParking = nightParking;
        this.vehicleHeight = vehicleHeight;
    }

    public IQueryable<Carpark> Query(DbSet<Carpark> set)
    {
        var query = set.AsQueryable();
        query = query.Include(x => x.CarParkType)
            .Include(x => x.FreeParkingType)
            .Include(x => x.ParkingSystemType)
            .Include(x => x.ShortTermParkingType);

        if (freeParking.HasValue)
        {
            if (freeParking.Value)
            {
                query = query.Where(x => x.FreeParkingType!.Name != "NO");
            }
            else
            {
                query = query.Where(x => x.FreeParkingType!.Name == "NO");
            }
        }

        if (nightParking.HasValue)
        {
            query = query.Where(x => x.NightParking == nightParking.Value);
        }

        if (vehicleHeight.HasValue)
        {
            query = query.Where(x => x.GantryHeight == 0 || x.GantryHeight > vehicleHeight.Value);
        }

        return query;
    }
}

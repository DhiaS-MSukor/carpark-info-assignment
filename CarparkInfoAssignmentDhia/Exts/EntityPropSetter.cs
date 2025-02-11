using CarparkInfoAssignmentDhia.CarparkInfo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CarparkInfoAssignmentDhia.Exts;
public static class EntityPropSetter
{
    public static void SetCarParkType(this Carpark carpark, CarParkType? carParkType)
    {
        carpark.CarParkType = carParkType;
        if (carParkType is not null)
        {
            carpark.CarParkType_Id = carParkType.Id;
        }
    }

    public static void SetFreeParkingType(this Carpark carpark, FreeParkingType? freeParkingType)
    {
        carpark.FreeParkingType = freeParkingType;
        if (freeParkingType is not null)
        {
            carpark.FreeParkingType_Id = freeParkingType.Id;
        }
    }

    public static void SetParkingSystemType(this Carpark carpark, ParkingSystemType? parkingSystemType)
    {
        carpark.ParkingSystemType = parkingSystemType;
        if (parkingSystemType is not null)
        {
            carpark.ParkingSystemType_Id = parkingSystemType.Id;
        }
    }

    public static void SetShortTermParkingType(this Carpark carpark, ShortTermParkingType? shortTermParkingType)
    {
        carpark.ShortTermParkingType = shortTermParkingType;
        if (shortTermParkingType is not null)
        {
            carpark.ShortTermParkingType_Id = shortTermParkingType.Id;
        }
    }
}

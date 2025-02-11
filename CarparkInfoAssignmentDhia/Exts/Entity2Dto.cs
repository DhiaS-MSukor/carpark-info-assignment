using CarparkInfoAssignmentDhia.CarparkInfo.Entities;
using CarparkInfoAssignmentDhia.Dtos;
using System.Net;

namespace CarparkInfoAssignmentDhia.Exts;

public static class Entity2Dto
{
    public static CsvDto ToCsvDto(this Carpark carpark)
    {
        return new CsvDto
        (
          carpark.CarParkNo,
          carpark.Address,
          carpark.XCoord.ToString(),
          carpark.YCoord.ToString(),
          carpark.CarParkType.Name,
          carpark.ParkingSystemType.Name,
          carpark.ShortTermParkingType.Name,
          carpark.FreeParkingType.Name,
          carpark.NightParking.ToString(),
          carpark.CarParkDecks.ToString(),
          carpark.GantryHeight.ToString(),
           carpark.CarParkBasement.ToString()
        );
    }
}

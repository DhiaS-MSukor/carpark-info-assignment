using CarparkInfoAssignmentDhia.CarparkInfo.Dtos;
using CarparkInfoAssignmentDhia.CarparkInfo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CarparkInfoAssignmentDhia.CarparkInfo.Exts;
public static class Dto2Entity
{
    public static Carpark ToCarpark(this CsvDto dto)
    {
        static bool string2Bool(string str)
        {
            ArgumentNullException.ThrowIfNull(str);

            if (string.IsNullOrWhiteSpace(str))
            {
                throw new ArgumentException("String must not be empty or whitespace", nameof(str));
            }

            return str.ToUpperInvariant() switch
            {
                "YES" or "Y" => true,
                "NO" or "N" => false,
                _ => throw new ArgumentException("Unacceptable string value", nameof(str)),
            };
        }

        return new Carpark
        {
            Address = dto.address,
            CarParkBasement = string2Bool(dto.car_park_basement),
            CarParkDecks = int.Parse(dto.car_park_decks),
            CarParkNo = dto.car_park_no,
            GantryHeight = decimal.Parse(dto.gantry_height),
            NightParking = string2Bool(dto.night_parking),
            XCoord = decimal.Parse(dto.x_coord),
            YCoord = decimal.Parse(dto.y_coord),
        };
    }

    public static Carpark MergeRecord(this Carpark carpark, CsvDto dto)
    {
        var dtoCarpark = dto.ToCarpark();

        carpark.Address = dtoCarpark.Address;
        carpark.CarParkBasement = dtoCarpark.CarParkBasement;
        carpark.CarParkDecks = dtoCarpark.CarParkDecks;
        carpark.CarParkNo = dtoCarpark.CarParkNo;
        carpark.GantryHeight = dtoCarpark.GantryHeight;
        carpark.NightParking = dtoCarpark.NightParking;
        carpark.XCoord = dtoCarpark.XCoord;
        carpark.YCoord = dtoCarpark.YCoord;

        return carpark;
    }
}

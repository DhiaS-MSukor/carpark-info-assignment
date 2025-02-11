using CarparkInfoAssignmentDhia.CarparkInfo.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace CarparkInfoAssignmentDhia.CarparkInfo.Entities;

public class Carpark
{
    public int Id { get; set; }
    public int CarParkType_Id { get; set; }
    public int ParkingSystemType_Id { get; set; }
    public int ShortTermParkingType_Id { get; set; }
    public int FreeParkingType_Id { get; set; }
    public string? CarParkNo { get; set; }
    public string? Address { get; set; }
    public decimal XCoord { get; set; }
    public decimal YCoord { get; set; }
    public bool NightParking { get; set; }
    public int CarParkDecks { get; set; }
    public decimal GantryHeight { get; set; }
    public bool CarParkBasement { get; set; }
    public virtual CarParkType? CarParkType { get; set; }
    public virtual ParkingSystemType? ParkingSystemType { get; set; }
    public virtual ShortTermParkingType? ShortTermParkingType { get; set; }
    public virtual FreeParkingType? FreeParkingType { get; set; }
    public virtual ICollection<UserFavoriteCarpark> UserFavoriteCarparks { get; set; } = new HashSet<UserFavoriteCarpark>();
}

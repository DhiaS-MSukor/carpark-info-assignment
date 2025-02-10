﻿namespace CarparkInfoAssignmentDhia.CarparkInfo.Entities;

public class ParkingSystemType
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public virtual ICollection<Carpark> Carparks { get; set; } = new HashSet<Carpark>();
}
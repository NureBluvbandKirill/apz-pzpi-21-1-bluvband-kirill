namespace ThermoTsev.Backend.Domain.Entities;

public class Location : BaseEntity
{
    public float Latitude { get; set; }

    public float Longitude { get; set; }

    public List<Shipment> StartShipments { get; set; }

    public List<Shipment> EndShipments { get; set; }
}

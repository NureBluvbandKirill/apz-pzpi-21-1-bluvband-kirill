using ThermoTsev.Backend.Domain.Enums;

namespace ThermoTsev.Backend.Domain.Entities;

public class Shipment : BaseEntity
{
    public DateTime StartDate { get; set; }

    public int StartLocationId { get; set; }

    public Location StartLocation { get; set; }

    public DateTime EndDate { get; set; }

    public int EndLocationId { get; set; }

    public Location EndLocation { get; set; }

    public ShipmentCondition ShipmentCondition { get; set; }

    public ShipmentStatus Status { get; set; }

    public User User { get; set; }

    public List<Analytic> Analytics { get; set; }
}

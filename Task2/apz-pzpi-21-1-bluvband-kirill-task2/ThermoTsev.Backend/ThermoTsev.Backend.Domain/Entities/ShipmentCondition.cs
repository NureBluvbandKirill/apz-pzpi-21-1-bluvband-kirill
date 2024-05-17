namespace ThermoTsev.Backend.Domain.Entities;

public class ShipmentCondition : BaseEntity
{
    public float MinTemperature { get; set; }

    public float MaxTemperature { get; set; }

    public float MinHumidity { get; set; }

    public float MaxHumidity { get; set; }

    public int ShipmentId { get; set; }

    public Shipment Shipment { get; set; }
}

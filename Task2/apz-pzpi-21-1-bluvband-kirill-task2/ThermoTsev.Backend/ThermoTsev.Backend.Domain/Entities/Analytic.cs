namespace ThermoTsev.Backend.Domain.Entities;

public class Analytic : BaseEntity
{
    public string Metric { get; set; }

    public string Value { get; set; }

    public DateTime Timestamp { get; set; }

    public Shipment Shipment { get; set; }
}

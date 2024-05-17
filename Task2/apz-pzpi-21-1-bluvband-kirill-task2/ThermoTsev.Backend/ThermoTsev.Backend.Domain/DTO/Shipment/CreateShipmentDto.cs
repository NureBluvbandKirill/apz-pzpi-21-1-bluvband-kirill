namespace ThermoTsev.Backend.Domain.DTO.Shipment;

public record CreateShipmentDto
{
    public DateTime StartDate { get; set; }
    
    public float StartLocationLatitude { get; set; }
    
    public float StartLocationLongitude { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public float EndLocationLatitude { get; set; }
    
    public float EndLocationLongitude { get; set; }
    
    public float MinTemperature { get; set; }
    
    public float MaxTemperature { get; set; }
    
    public float MinHumidity { get; set; }
    
    public float MaxHumidity { get; set; }
};

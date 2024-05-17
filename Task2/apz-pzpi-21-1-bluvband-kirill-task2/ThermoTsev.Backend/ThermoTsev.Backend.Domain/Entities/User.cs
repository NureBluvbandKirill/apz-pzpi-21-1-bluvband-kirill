using ThermoTsev.Backend.Domain.Enums;

namespace ThermoTsev.Backend.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; }

    public string Email { get; set; }

    public string PasswordHashed { get; set; }

    public string PasswordSalt { get; set; }

    public Role Role { get; set; }

    public List<Shipment> Shipments { get; set; }

    public List<Notification> Notifications { get; set; }
}

namespace ThermoTsev.Backend.Domain.Entities;

public class Notification : BaseEntity
{
    public string Message { get; set; }

    public bool isRead { get; set; }

    public User User { get; set; }
}

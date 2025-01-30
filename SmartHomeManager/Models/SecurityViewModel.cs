using SmartHomeManager.Models;

public class SecurityViewModel
{
    public string UserId { get; set; }
    public IEnumerable<Device> Devices { get; set; }
    public IEnumerable<AccessLevel> AccessLevels { get; set; }
}
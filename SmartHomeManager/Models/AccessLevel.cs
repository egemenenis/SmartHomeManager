namespace SmartHomeManager.Models
{
    public class AccessLevel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Device> Devices { get; set; }
    }
}
namespace SmartHomeManager.Models
{
    public class Device
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsOn { get; set; }
        public int? RoomId { get; set; }
        public Room? Room { get; set; }
        public string DeviceType { get; set; }
        public bool IsTwoFactorEnabled { get; set; }
        public int? AccessLevelId { get; set; }
        public AccessLevel? AccessLevel { get; set; }
        public string? ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
    }
}
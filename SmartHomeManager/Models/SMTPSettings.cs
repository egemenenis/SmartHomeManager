namespace SmartHomeManager.Services
{
    public class SMTPSettings
    {
        public string SenderEmail { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
}
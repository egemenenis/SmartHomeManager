namespace SmartHomeManager.Models
{
    public class MailRequest
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string ReceiverEmail { get; set; }
    }
}
namespace HotelBackend.Identity.Common.Settings
{
    public class EmailSenderSettings
    {
        public string From { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string SmtpServer { get; set; } = null!;
        public int Port { get; set; }
        public string UserName { get; set; } = null!;
        public string AppPassword { get; set; } = null!;
    }
}

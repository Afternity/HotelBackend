namespace HotelBackend.Identity.Common.Interfaces
{
    public interface IEmailSender
    {
        Task SendAsync(
            string to,
            string subject,
            string body,
            CancellationToken cancellationToken);
    }
}

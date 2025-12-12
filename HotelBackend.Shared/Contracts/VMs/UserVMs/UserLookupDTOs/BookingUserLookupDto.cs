namespace HotelBackend.Shared.Contracts.VMs.UserVMs.UserLookupDTOs
{
    public class BookingUserLookupDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}

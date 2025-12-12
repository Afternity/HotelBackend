namespace HotelBackend.Shared.Contracts.VMs.UserVMs.UserLookupDTOs
{
    public class UserLookupDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public Guid UserTypeId { get; set; }
    }
}

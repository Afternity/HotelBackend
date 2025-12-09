namespace HotelBackend.Shared.Contracts.ViewModels.UserViewModes
{
    public class UserLookupDto
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public Guid UserTypeId { get; set; }
    }
}

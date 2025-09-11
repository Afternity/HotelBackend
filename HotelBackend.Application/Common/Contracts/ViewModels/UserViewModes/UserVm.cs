namespace HotelBackend.Application.Common.Contracts.ViewModels.UserViewModes
{
    public class UserVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public Guid UserTypeId { get; set; }
    }
}

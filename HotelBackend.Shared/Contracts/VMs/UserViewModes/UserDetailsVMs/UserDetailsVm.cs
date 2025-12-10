namespace HotelBackend.Shared.Contracts.VMs.UserViewModes.UserDetailsVMs
{
    public class UserDetailsVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public Guid UserTypeId { get; set; }
    }
}

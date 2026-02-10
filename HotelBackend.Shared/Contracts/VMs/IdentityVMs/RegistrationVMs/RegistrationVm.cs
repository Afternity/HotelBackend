namespace HotelBackend.Shared.Contracts.VMs.IdentityVMs.RegistrationVMs
{
    public class RegistrationVm
    {
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool EmailConfirmationRequired { get; set; }
        public string Message { get; set; } = null!;
    }
}

namespace HotelBackend.Shared.Contracts.ViewModels.UserViewModes
{
    public class UserListVm
    {
        public IList<UserLookupDto> Users { get; set; } = [];
    }
}

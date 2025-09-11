using HotelBackend.Domain.Models;

namespace HotelBackend.Application.Common.Contracts.ViewModels.UserViewModes
{
    public class UserListVm
    {
        public IList<UserLookupDto> Users { get; set; } = [];
    }
}

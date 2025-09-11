using HotelBackend.Domain.BaseModels;

namespace HotelBackend.Domain.Models
{
    public class User : BaseModel
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public Guid UserTypeId { get; set; }
        public UserType UserType { get; set; } = null!;
        public IList<Reservation> Reservations { get; set; } = [];
    }
}

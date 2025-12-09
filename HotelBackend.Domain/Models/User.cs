using HotelBackend.Domain.BaseModels;

namespace HotelBackend.Domain.Models
{
    public class User 
        : BaseModel
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public Guid UserTypeId { get; set; }
        public virtual UserType UserType { get; set; } = null!;
        public virtual ICollection<Booking> Reservations { get; set; } = [];
        public virtual ICollection<Review> Reviews { get; set; } = [];
    }
}

using HotelBackend.Domain.BaseModels;
using HotelBackend.Domain.Emums.RoomEnums;

namespace HotelBackend.Domain.Models
{
    public class Room 
        : BaseModel
    {
        public string Number { get; set; } = null!;
        public RoomClass Class { get; set; } = RoomClass.Standard;
        public string Description { get; set; } = string.Empty;
        public decimal PricePerNight { get; set; } = decimal.Zero;

        public virtual ICollection<Booking> Reservations { get; set; } = [];
        public virtual ICollection<Review> Reviews { get; set; } = [];
    }
}

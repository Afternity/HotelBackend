using HotelBackend.Domain.BaseModels;

namespace HotelBackend.Domain.Models
{
    public class Room : BaseModel
    {
        public string Number { get; set; } = null!;
        public string Class { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public decimal PricePerNight { get; set; } = decimal.Zero;
        public IList<Reservation> Reservations { get; set; } = [];
    }
}

namespace HotelBackend.Shared.Contracts.ViewModels.RoomViewModels
{
    public class RoomLookupDto
    {
        public string Number { get; set; } = null!;
        public string Class { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public decimal PricePerNight { get; set; } = decimal.Zero;
    }
}

namespace HotelBackend.Shared.Contracts.ViewModels.RoomViewModels
{
    public class RoomVm
    {
        public Guid Id { get; set; }
        public string Number { get; set; } = null!;
        public string Class { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public decimal PricePerNight { get; set; } = decimal.Zero;
    }
}

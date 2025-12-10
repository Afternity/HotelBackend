namespace HotelBackend.Shared.Contracts.VMs.RoomVMs.RoomLookupDTOs
{
    public class RoomLookupDto
    {
        public Guid Id { get; set; }
        public string Number { get; set; } = string.Empty;
        public string Class { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal PricePerNight { get; set; } 
    }
}

namespace HotelBackend.Shared.Contracts.VMs.BookingVMs.BookingDetailsVMs
{
    public class BookingDetailsVm
    {
        public Guid Id { get; set; }
        public DateTime CheckInDate { get; set; } 
        public DateTime CheckOutDate { get; set; } 

        public Guid RoomId { get; set; }
        public Guid UserId { get; set; }
    }
}

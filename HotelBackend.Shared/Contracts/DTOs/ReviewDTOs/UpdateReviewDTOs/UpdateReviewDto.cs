namespace HotelBackend.Shared.Contracts.DTOs.ReviewDTOs.UpdateReviewDTOs
{
    public record UpdateReviewDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; } = string.Empty; 
        public int Rating { get; set; } 

        public Guid RoomId { get; set; }
        public Guid UserId { get; set; }
        public Guid BookingId { get; set; }
    }
}

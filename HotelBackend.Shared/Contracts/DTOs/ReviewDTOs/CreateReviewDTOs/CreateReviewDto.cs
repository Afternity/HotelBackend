namespace HotelBackend.Shared.Contracts.DTOs.ReviewDTOs.CreateReviewDTOs
{
    public record CreateReviewDto
    {
        public string Text { get; set; } = string.Empty;
        public int Rating { get; set; } 

        public Guid BookingId { get; set; }
    }
}

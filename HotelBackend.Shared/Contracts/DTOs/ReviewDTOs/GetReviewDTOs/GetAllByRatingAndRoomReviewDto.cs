namespace HotelBackend.Shared.Contracts.DTOs.ReviewDTOs.GetReviewDTOs
{
    public record GetAllByRatingAndRoomReviewDto
    {
        public int Rating { get; set; }
        public Guid RoomId { get; set; }
    }
}

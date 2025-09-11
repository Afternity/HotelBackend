namespace HotelBackend.Application.Common.Contracts.DTOs.UserTypeDTOs
{
    public class UpdateUserTypeDto
    {
        public Guid Id { get; set; }
        public string Type { get; set; } = null!;
    }
}

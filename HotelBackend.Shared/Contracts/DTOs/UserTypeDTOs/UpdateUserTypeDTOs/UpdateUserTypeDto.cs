namespace HotelBackend.Shared.Contracts.DTOs.UserTypeDTOs.UpdateUserTypeDTOs
{
    public record UpdateUserTypeDto
    {
        public Guid Id { get; set; }
        public string Type { get; set; } = string.Empty;
    }
}

namespace HotelBackend.Shared.Contracts.DTOs.UserTypeDTOs.UpdareUserTypeDTOs
{
    public record UpdateUserTypeDto
    {
        public Guid Id { get; set; }
        public string Type { get; set; } = string.Empty;
    }
}

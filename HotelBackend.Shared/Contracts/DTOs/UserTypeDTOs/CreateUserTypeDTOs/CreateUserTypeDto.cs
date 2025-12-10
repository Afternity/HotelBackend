namespace HotelBackend.Shared.Contracts.DTOs.UserTypeDTOs.CreateUserTypeDTOs
{
    public record CreateUserTypeDto
    {
        public string Type { get; set; } = string.Empty;
    }
}

namespace HotelBackend.Application.Common.Contracts.DTOs.UserDTOs
{
    public class CreateUserDto
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public Guid UserTypeId { get; set; } 
    }
}

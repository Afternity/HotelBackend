namespace HotelBackend.Application.Common.Contracts.DTOs.UserDTOs
{
    public class UpdateUserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public Guid UserTypeId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace HotelBackend.Shared.Contracts.DTOs.IdentityDTOs.RegistrationDTOs
{
    public class RegistrationDto
    {
        public string Email { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string Password { get; set; } = null!;
        public string RepeatPassword { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? MiddleName { get; set; }
    }
}

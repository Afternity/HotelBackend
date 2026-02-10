using Microsoft.AspNetCore.Identity;

namespace HotelBackend.Identity.Common.Models
{
    public class ApplicationUser
        : IdentityUser<long>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}

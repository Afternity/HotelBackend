namespace HotelBackend.Identity.Common.Settings
{
    public class JwtSettings
    {
        public string Secret { get; set; } = null!;
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;

        /// <summary>
        /// Время жизни Access Token в минутах
        /// </summary>
        public int Expire { get; set; }

        /// <summary>
        /// Время жизни Refresh Token в днях
        /// </summary>
        public int RefreshTokenValidityInDays { get; set; }
    }
}

namespace HotelBackend.Shared.Contracts.Containers.IdentityContainers
{
    public readonly record struct JwtTokensContainer
    {
        public string AccessToken { get; }
        public string RefreshToken { get; }

        public JwtTokensContainer(
            string accessToken,
            string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
                throw new ArgumentException("Access token не должен быть пустым", nameof(accessToken));

            if (string.IsNullOrWhiteSpace(refreshToken))
                throw new ArgumentException("Refresh token не должен быть пустым", nameof(refreshToken));

            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}

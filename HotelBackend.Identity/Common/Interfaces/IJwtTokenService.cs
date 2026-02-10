using HotelBackend.Identity.Common.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace HotelBackend.Identity.Common.Interfaces
{
    public interface IJwtTokenService
    {
        DateTime RefreshTokenValidityInDays { get; }

        /// <summary>
        /// Только для создания access token
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        string CreateAccessToken(
            ApplicationUser user,
            IList<string> roles);

        /// <summary>
        /// Перегрузка для пересоздания access token
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        string CreateAccessToken(
            ClaimsPrincipal principal);
        
        /// <summary>
        /// Только для создания refresh token
        /// </summary>
        /// <returns></returns>
        string CreateRefreshToken();

        /// <summary>
        /// Только для валидации просроченного access token. refresh flow
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        ClaimsPrincipal? ValidateExpiredAccessToken(
            string expiredAccessToken);

        /// <summary>
        /// Только для проверки валидности token.
        /// Был вариант сделать static в классе, но я решил сделать так.
        /// </summary>
        /// <param name="userRefreshToken"></param>
        /// <param name="userRefreshTokenExpiryTime"></param>
        /// <param name="otherRefreshToken"></param>
        /// <returns></returns>
        static bool CheckValidRefreshToken(
            string userRefreshToken,
            DateTime userRefreshTokenExpiryTime,
            string otherRefreshToken)
        {
            if (userRefreshToken != otherRefreshToken)
                return false;

            if (userRefreshTokenExpiryTime <= DateTime.UtcNow)
                return false;

            return true;
        }
    }
}

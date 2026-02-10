using HotelBackend.Identity.Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Identity.Data.DbContexts
{
    public class HotelBackendIdentityDbContext 
        : IdentityDbContext<ApplicationUser, IdentityRole<long>, long>
    {
        private readonly DbContextOptions<HotelBackendIdentityDbContext> _options;

        public HotelBackendIdentityDbContext(
            DbContextOptions<HotelBackendIdentityDbContext> options)
            : base(options)
        {
            _options = options;
        }
    }
}

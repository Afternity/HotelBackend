using HotelBackend.Domain.BaseModels;

namespace HotelBackend.Domain.Models
{
    public class UserType 
        : BaseModel
    {
        public string Type { get; set; } = null!;

        public virtual IList<User> Users { get; set; } = [];
    }
}

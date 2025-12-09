namespace HotelBackend.Domain.BaseModels
{
    public class BaseModel
    {
        public Guid Id { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdateAt { get; set; } 

        public bool IsDeleted { get; set; } = false;
        public DateTime DeletedAt { get; set; } 

    }
}

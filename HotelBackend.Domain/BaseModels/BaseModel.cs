namespace HotelBackend.Domain.BaseModels
{
    /// <summary>
    /// Все модели БД наследуют от BaseModel
    /// </summary>
    public class BaseModel
    {
        public Guid Id { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdateAt { get; set; } 

        public bool IsDeleted { get; set; } = false;
        public DateTime DeletedAt { get; set; } 

    }
}

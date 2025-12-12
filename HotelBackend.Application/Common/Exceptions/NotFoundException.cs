namespace HotelBackend.Application.Common.Exceptions
{
    /// <summary>
    /// Решил перейти на возврат null значения. Пусть там на UI разбераются.
    /// На момент первого переписывания Services не используестся в коде.
    /// </summary>
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) not found.") { }
    }
}

namespace HotelBackend.Application.Common.Exceptions
{
    /// <summary>
    /// Нет времени это реазовывать на момент переписывания Services.
    /// </summary>
    public class IsDublicateException : Exception
    {
        public IsDublicateException(string name, object key)
            : base($"Entity \"{name}\" ({key}) is dublicate.") { }
    }
}

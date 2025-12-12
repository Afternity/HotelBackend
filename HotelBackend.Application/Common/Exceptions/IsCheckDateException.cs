namespace HotelBackend.Application.Common.Exceptions
{
    /// <summary>
    /// Нет времени это реазовывать на момент переписывания Services.
    /// </summary>
    public class IsCheckDateException : Exception
    {
        public IsCheckDateException(string name, object key)
            : base($"Entity \"{name}\" ({key}) wrong check date.") { }
    }
}

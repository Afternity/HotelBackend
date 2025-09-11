namespace HotelBackend.Application.Common.Exceptions
{
    public class IsCheckDateException : Exception
    {
        public IsCheckDateException(string name, object key)
            : base($"Entity \"{name}\" ({key}) wrong check date.") { }
    }
}

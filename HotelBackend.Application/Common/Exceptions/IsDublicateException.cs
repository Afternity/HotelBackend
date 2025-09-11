namespace HotelBackend.Application.Common.Exceptions
{
    public class IsDublicateException : Exception
    {
        public IsDublicateException(string name, object key)
            : base($"Entity \"{name}\" ({key}) is dublicate.") { }
    }
}

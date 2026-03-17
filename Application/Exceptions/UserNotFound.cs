namespace gastronomiya.Application.Exceptions
{
    public class UserNotFound : Exception
    {
        public UserNotFound(string message) : base(message) { }
    }
}

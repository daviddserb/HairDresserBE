namespace hairDresser.Application.CustomExceptions
{
    public abstract class ClientCustomExceptionBase : Exception
    {
        public ClientCustomExceptionBase(string message) : base(message) { }
    }
}

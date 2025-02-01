namespace hairDresser.Application.CustomExceptions
{
    public class ClientException : ClientCustomExceptionBase
    {
        public ClientException(string message) : base(message) { }
    }
}

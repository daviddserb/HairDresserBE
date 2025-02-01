namespace hairDresser.Application.CustomExceptions
{
    public class NotFoundException : ClientCustomExceptionBase
    {
        public NotFoundException(string message) : base(message) { }
    }
}

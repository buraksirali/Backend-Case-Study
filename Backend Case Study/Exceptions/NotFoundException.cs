namespace Backend_Case_Study.Exceptions
{
    public class NotFoundException : Exception
    {
        public readonly string errorMessage;
        public NotFoundException(string errorMessage)
        {
            this.errorMessage = errorMessage;
        }
    }
}

namespace Backend_Case_Study.Exceptions
{
    public class MultiplePoolException : Exception
    {
        public readonly string errorMessage;
        public MultiplePoolException(string errorMessage)
        {
            this.errorMessage = errorMessage;
        }
    }
}

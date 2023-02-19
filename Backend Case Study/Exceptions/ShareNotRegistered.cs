namespace Backend_Case_Study.Exceptions
{
    public class ShareNotRegistered : Exception
    {
        public readonly string errorMessage;
        public ShareNotRegistered(string errorMessage)
        {
            this.errorMessage = errorMessage;
        }
    }
}

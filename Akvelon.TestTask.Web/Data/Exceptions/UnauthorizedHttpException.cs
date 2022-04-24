namespace Akvelon.TestTask.Web.Data.Exceptions
{
    public class UnauthorizedHttpException : Exception
    {
        public UnauthorizedHttpException(string message) : base(message)
        {
        }
    }
}
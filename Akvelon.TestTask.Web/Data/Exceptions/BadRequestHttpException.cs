namespace Akvelon.TestTask.Web.Data.Exceptions
{
    public class BadRequestHttpException : Exception
    {
        public BadRequestHttpException(string message): base(message)
        {}
    }
}
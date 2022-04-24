namespace Akvelon.TestTask.Web.Data.Exceptions
{
    public class NotFoundHttpException : Exception
    {
        public NotFoundHttpException(string message) : base(message)
        {
        }
    }
}
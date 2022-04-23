namespace Akvelon.TestTask.Web.Data.Exceptions
{
    public class ConflictHttpException: Exception
    {
        public ConflictHttpException(string message): base(message)
        {}
    }
}
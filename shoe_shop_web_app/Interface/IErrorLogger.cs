namespace shoe_shop_web_app.Interface
{
    public interface IErrorLogger
    {
        void LogError(Exception ex, string infoMessage);
    }
    public class ErrorLogger : IErrorLogger
    {
        public void LogError(Exception ex, string infoMessage)
        {
            //Log the error to your error database
        }
    }
}

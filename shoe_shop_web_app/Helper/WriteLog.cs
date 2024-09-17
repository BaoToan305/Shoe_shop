namespace shoe_shop_web_app.Helper
{
    public static class WriteLog
    {

        public static void logs(string message)
        {

            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine(message);
            }
        }
    }
}

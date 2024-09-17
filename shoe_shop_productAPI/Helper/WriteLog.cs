namespace shoe_shop_productAPI.Helper
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

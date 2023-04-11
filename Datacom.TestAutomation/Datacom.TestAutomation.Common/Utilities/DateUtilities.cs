namespace Datacom.TestAutomation.Common
{
    public static class DateUtilities
    {
        public static string GetCurrentDateTime(string pattern)
        {
            //Sample format: "yyyy-MM-dd'T'HH:mm:ss.fff"
            return DateTime.Now.ToString(pattern);
        }

        public static DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }
    }
}

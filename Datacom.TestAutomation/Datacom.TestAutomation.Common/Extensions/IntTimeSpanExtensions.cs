namespace Datacom.TestAutomation.Common
{
    public static class IntTimeSpanExtensions
    {
        public static TimeSpan Milliseconds(this int time)
        {
            return TimeSpan.FromMilliseconds(time);
        }
        public static TimeSpan Seconds(this int time)
        {
            return TimeSpan.FromSeconds(time);
        }

        public static TimeSpan Minutes(this int time)
        {
            return TimeSpan.FromMinutes(time);
        }
    }
}

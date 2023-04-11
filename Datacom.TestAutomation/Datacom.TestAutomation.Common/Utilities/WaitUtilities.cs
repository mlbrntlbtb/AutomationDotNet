using System.Globalization;

namespace Datacom.TestAutomation.Common
{
    public static class WaitUtilities
    {
        public static void Until(Func<bool> condition, TimeSpan timeout, string message)
        {
            Until(condition, timeout, 500.Milliseconds(), message);
        }

        public static void Until(Func<bool> condition, TimeSpan timeout, TimeSpan sleepInterval, string message)
        {
            var result = Until(condition, timeout, sleepInterval);

            if (!result)
            {
                throw new WaitTimeoutException(string.Format(CultureInfo.CurrentCulture, "Timeout after {0} second(s), {1}", timeout.TotalSeconds, message));
            }
        }

        public static bool Until(Func<bool> condition, TimeSpan timeout)
        {
            return Until(condition, timeout, 500.Milliseconds());
        }

        public static bool Until(Func<bool> condition, TimeSpan timeout, TimeSpan sleepInterval)
        {
            var result = false;
            var start = DateTime.Now;
            var canceller = new CancellationTokenSource();
            var task = Task.Factory.StartNew(condition, canceller.Token);

            while ((DateTime.Now - start).TotalSeconds < timeout.TotalSeconds)
            {
                if (task.IsCompleted)
                {
                    if (task.Result)
                    {
                        result = true;
                        canceller.Cancel();
                        break;
                    }

                    canceller.Cancel();
                    canceller.Dispose();

                    canceller = new CancellationTokenSource();
                    task = Task.Factory.StartNew(condition, canceller.Token);
                }

                Thread.Sleep(sleepInterval);
            }

            canceller.Cancel();
            canceller.Dispose();
            return result;
        }
    }
}

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Datacom.TestAutomation.Web.Selenium
{
    public class TryWebDriverWait : WebDriverWait
    {
        public TryWebDriverWait(IWebDriver driver, TimeSpan timeout) : base(driver, timeout)
        {
        }

        public TryWebDriverWait(IClock clock, IWebDriver driver, TimeSpan timeout, TimeSpan sleepInterval) : base(clock, driver, timeout, sleepInterval)
        {
        }

        public bool Until(Func<IWebDriver, bool> condition)
        {
            try
            {
                return base.Until(condition);
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public override TResult Until<TResult>(Func<IWebDriver, TResult> condition)
        {
            try
            {
                return base.Until(condition);
            }
            catch (WebDriverTimeoutException)
            {
                return default!;
            }
        }

        public override TResult Until<TResult>(Func<IWebDriver, TResult> condition, CancellationToken token)
        {
            try
            {
                return base.Until(condition, token);
            }
            catch (WebDriverTimeoutException)
            {
                return default!;
            }
        }
    }
}

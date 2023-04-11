using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;

namespace BNZ.TestAutomation.Sample.Model
{
    public abstract class BasePage<T> : Loadable<T>, ILoadable<T> 
        where T : Loadable<T>
    {
        private readonly IWebDriver driver;
        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public override bool Wait(TimeSpan timeout)
        {
            return driver.WaitForPageToLoad() && base.Wait(timeout);
        }

        protected override void ExecuteLoad()
        {
            driver.WaitForPageToLoad();
        }
    }
}

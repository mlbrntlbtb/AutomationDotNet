using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Tempo.TestAutomation.Model.Web
{
    public abstract class TempoBaseModal<T> : Loadable<T>, ILoadable<T>
        where T : LoadableComponent<T>
    {
        private readonly IWebDriver driver;

        public TempoBaseModal(IWebDriver driver)
        {
            this.driver = driver;
        }

        protected override void ExecuteLoad() => driver.WaitForPageToLoad();

        public bool IsModalPresent()
        {
            bool IsOverlayPresent = driver.FindElements(By.CssSelector(".k-overlay")).Any();
            bool IsModalWindowPresent = driver.FindElements(By.CssSelector("*[class*='k-window']")).Any();

            return IsOverlayPresent && IsModalWindowPresent;
        }
    }
}
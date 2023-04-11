using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Tempo.TestAutomation.Model.Web
{
    public abstract class TempoBasePage<T> : Loadable<T>, ILoadable<T>
        where T : LoadableComponent<T>
    {
        private readonly IWebDriver driver;

        public TempoBasePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        protected override void ExecuteLoad() => driver.WaitForPageToLoad();
    }
}
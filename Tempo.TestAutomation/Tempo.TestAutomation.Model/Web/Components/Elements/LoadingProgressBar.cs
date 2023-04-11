using OpenQA.Selenium;
using Datacom.TestAutomation.Web.Selenium;

namespace Tempo.TestAutomation.Model.Web.Components.Elements
{
    public class LoadingProgressBar
    {
        protected IWebDriver driver;
        private By loadingProgressBarLocator = By.CssSelector("div[class*='ng-progress'][class*='active']");

        public LoadingProgressBar(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void WaitToDisappear()
        {
            driver.WaitUntilElementIsNotPresent(loadingProgressBarLocator);
        }
    }
}

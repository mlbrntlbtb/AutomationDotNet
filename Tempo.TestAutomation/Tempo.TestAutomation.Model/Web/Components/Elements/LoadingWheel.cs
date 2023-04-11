using Datacom.TestAutomation.Common;
using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Tempo.TestAutomation.Model.Web.Components.Object
{
    public class LoadingWheel
    {
        protected IWebDriver driver;

        public LoadingWheel(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void WaitToDisappear()
        {
            driver.WaitUntilElementIsNotPresent(By.Id("loading-bar"));
        }
    }
}
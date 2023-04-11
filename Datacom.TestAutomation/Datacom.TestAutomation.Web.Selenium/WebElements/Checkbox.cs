using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Datacom.TestAutomation.Web.Selenium
{
    public class Checkbox : WebElement
    {
        private readonly IWebElement webElement;

        public Checkbox(IWebElement webElement)
            : base(webElement.ToDriver() as RemoteWebDriver, null)
        {
            this.webElement = webElement;
        }

        public void Tick()
        {
            if (!webElement.Selected)
            {
                webElement.Click();
            }
        }

        public void Untick()
        {
            if (webElement.Selected)
            {
                webElement.Click();
            }
        }
    }
}

using OpenQA.Selenium;

namespace Datacom.TestAutomation.Web.Selenium
{
    public class JavaScriptAlert
    {
        private readonly IWebDriver driver;
        public JavaScriptAlert(IWebDriver driver)
        {
            this.driver = driver;
        }

        public string Text
        {
            get { return driver.SwitchTo().Alert().Text; }
        }

        public void Confirm()
        {
            driver.SwitchTo().Alert().Accept();
            driver.SwitchTo().DefaultContent();
        }

        public void Dismiss()
        {
            driver.SwitchTo().Alert().Dismiss();
            driver.SwitchTo().DefaultContent();
        }

        public void SendKeys(string text)
        {
            driver.SwitchTo().Alert().SendKeys(text);
        }
    }
}

using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using Tempo.TestAutomation.Model.Interface;

namespace Tempo.TestAutomation.Model.Web.Components.Elements
{
    public class ToastMessage : IGenericPageObj
    {
        protected IWebDriver driver;
        private By closeLocator = By.CssSelector("button[class*='close']");
        private By contentLocator = By.CssSelector("span[ng-bind-html='message.content']");
        private By toastMessageLocator = By.CssSelector("li[class*='ng-toast'][class*='message']");

        public ToastMessage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void CloseToastMessage()
        {
            IWebElement parentElement = driver.GetElement(toastMessageLocator);
            parentElement!.GetElement(closeLocator).Click();
        }

        public string GetToastMessageValue()
        {
            IWebElement parentElement = driver.GetElement(toastMessageLocator);
            return parentElement!.GetElement(contentLocator).Text.Trim();
        }

        public void SelectByText(string referenceText)
        {
            IWebElement parentElement = driver.GetElement(toastMessageLocator);
            parentElement!.GetElement(contentLocator).Click();
        }
    }
}
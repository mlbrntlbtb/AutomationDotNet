using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using ServiceStack;
using Tempo.TestAutomation.Model.Interface;

namespace Tempo.TestAutomation.Model.Web.Components.PageContainers
{
    public class DropDownListContainer : IGenericPageObj
    {
        protected IWebDriver driver;
        private By ParentElementLocator = By.CssSelector("div[style*='display: block'] ul.k-reset, div[class*='multiselect open'] ul[style*='block'], kendo-list ul.k-reset");
        private By ListItemElement = By.CssSelector("li[role]");

        public DropDownListContainer(IWebDriver driver)
        {
            this.driver = driver;
        }

        public bool IsDropDownItemPresent(string referenceText)
        {
            referenceText = referenceText.ToLower().Trim();
            IWebElement ParentElement = driver.GetElement(ParentElementLocator);

            return ParentElement!.GetElements(ListItemElement)
                .Where(e => e.Text.ToLower().Trim().Contains(referenceText))
                .Any();
        }

        public bool IsDropDownPresent()
        {
            return driver.IsElementPresent(ParentElementLocator);
        }

        public void MouseHoverByText(string referenceText)
        {
            referenceText = referenceText.ToLower().Trim();
            IWebElement ParentElement = driver.GetElement(ParentElementLocator);

            ParentElement!.GetElements(ListItemElement)
                .Where(e => e.Text.ToLower().Trim().Contains(referenceText))
                .First()
                .Hover(driver);
        }

        public void SelectByText(string referenceText)
        {
            referenceText = referenceText.ToLower().Trim();
            driver.WaitAvailability(ParentElementLocator);
            IWebElement ParentElement = driver.GetElement(ParentElementLocator);

            ParentElement!.GetElements(ListItemElement)
                .Where(e => e.Text.ToLower().Trim().Contains(referenceText))
                .First()
                .Click();
        }

        public void SelectByExactText(string referenceText)
        {
            referenceText = referenceText.ToLower().Trim();
            driver.WaitAvailability(ParentElementLocator);
            IWebElement ParentElement = driver.GetElement(ParentElementLocator);

            ParentElement!.GetElements(ListItemElement)
                .Where(e => e.Text.ToLower().Trim().Equals(referenceText))
                .First()
                .Click();
        }
    }
}
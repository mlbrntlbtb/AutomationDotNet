using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using Tempo.TestAutomation.Model.Interface;

namespace Tempo.TestAutomation.Model.Web.Components.Elements
{
    public class Tab : IGenericPageObj
    {
        protected IWebDriver driver;
        protected IWebElement parentElement;
        protected IList<IWebElement> tabItems;
        private By tabItemsLocator => By.CssSelector("li[class*='item']");
        public Tab(IWebElement parentElement, IWebDriver driver)
        {
            this.driver = driver;
            this.parentElement = parentElement;
            tabItems = parentElement.GetElements(tabItemsLocator);
        }
        
        public bool IsTabItemPresent(string referenceText)
        {
            return tabItems
                .Where(t => t.Text.ToLower().Trim().Equals(referenceText.ToLower()))
                .Any();
        }

        public void SelectByPartialText(string partialText)
        {
            tabItems.Where(t => t.Text.ToLower().Trim().Contains(partialText.ToLower()))
                .FirstOrDefault()!
                .Click();
        }

        public void SelectByText(string referenceText)
        {
            tabItems.Where(t => t.Text.ToLower().Trim().Equals(referenceText.ToLower()))
                .FirstOrDefault()!
                .Click();
        }
    }
}
using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using Tempo.TestAutomation.Model.Interface;

namespace Tempo.TestAutomation.Model.Web.Components.PageContainers
{
    public class TempoHomePageLinksContainer : IPageElementContainer
    {
        protected IWebElement ParentElement;

        public TempoHomePageLinksContainer(IWebElement ParentElement)
        {
            this.ParentElement = ParentElement;
        }

        public IWebElement GetElementByText(string referenceText)
        {
            referenceText = referenceText.ToLower().Trim();
            return ParentElement.GetElements(By.CssSelector(".dashboard-flex-item"))
                .Where(e => e.FindElement(By.CssSelector("a.ng-binding")).Text.ToLower().Trim().Equals(referenceText))
                .First()
                .FindElement(By.CssSelector("a.ng-binding"));
        }
    }
}
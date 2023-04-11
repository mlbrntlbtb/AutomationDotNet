using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using Tempo.TestAutomation.Model.Interface;

namespace Tempo.TestAutomation.Model.Web
{
    public class EventsListContainer : IPageElementContainer
    {
        protected IWebElement ParentElement;

        public EventsListContainer(IWebElement parentElement)
        {
            ParentElement = parentElement;
        }

        public IWebElement GetElementByText(string referenceText)
        {
            referenceText = referenceText.ToLower().Trim();
            return ParentElement.GetElements(By.CssSelector(".k-selectable"))
                .Where(e => e.FindElement(By.CssSelector("tbody tr:nth-child(1) td:nth-child(2)")).Text.ToLower().Trim().Equals(referenceText))
                .First()
                .FindElement(By.CssSelector("tbody tr:nth-child(1) td:nth-child(2)"));
        }
    }
}
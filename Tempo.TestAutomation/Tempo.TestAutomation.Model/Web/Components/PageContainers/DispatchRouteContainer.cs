using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using ServiceStack;
using Tempo.TestAutomation.Model.Interface;

namespace Tempo.TestAutomation.Model.Web
{
    public class DispatchRouteContainer : IPageElementContainer
    {
        protected IWebElement ParentElement;
        private By ListElementLocator = By.CssSelector("ul[kendo-list-view='dispatchRouteDetail']");

        public DispatchRouteContainer(IWebElement parentElement)
        {
            ParentElement = parentElement;
        }

        public IWebElement GetElementByText(string referenceText)
        {
            referenceText = referenceText.ToLower().Trim();
            return ParentElement.GetElements(By.CssSelector(".dispatch-route"))
                .Where(e => e.FindElement(By.CssSelector(".route-name")).Text.ToLower().Trim().Contains(referenceText))
                .First();
        }

        public string GetRouteJobActiveLabel() => ParentElement
            .GetElement(DispatchRouteContainerLocators.Text.ActiveLabel)
            .Text;

        public string GetRouteJobCount() => ParentElement
            .GetElement(DispatchRouteContainerLocators.Text.RouteJobCount)
            .Text;

        public string GetRouteName()
        {
            return ParentElement.GetElement(By.CssSelector(".route-name")).Text;
        }

        public List<IWebElement> GetRouteDetailCards(TimeSpan waitSeconds)
        {
            /* To access dispatch route detail cards, set DispatchRouteContainerLocators.RouteDetail.Container as ParentElement */
            return ParentElement.GetElements(DispatchRouteContainerLocators.RouteDetail.Card, waitSeconds)
                .ToList();
        }

        public DispatchRouteContainer Load() => this;
    }
}
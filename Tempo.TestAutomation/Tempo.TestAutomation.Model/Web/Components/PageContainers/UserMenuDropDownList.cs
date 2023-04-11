using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using Tempo.TestAutomation.Model.Interface;

namespace Tempo.TestAutomation.Model.Web.Components.PageContainers
{
    public class UserMenuDropDownList : IPageElementContainer
    {
        protected IWebElement ParentElement;

        public UserMenuDropDownList(IWebElement parentElement)
        {
            ParentElement = parentElement;
        }

        public IWebElement GetElementByText(string referenceText)
        {
            referenceText = referenceText.ToLower().Trim();
            return ParentElement.GetElements(By.CssSelector(".dropdown-menu div[ng-click]"))
                .Where(e => e.Text.ToLower().Trim().Contains(referenceText))
                .First();
        }
    }
}
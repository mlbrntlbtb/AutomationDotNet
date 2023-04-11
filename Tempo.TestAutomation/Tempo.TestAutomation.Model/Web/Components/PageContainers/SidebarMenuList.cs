using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using Tempo.TestAutomation.Model.Interface;

namespace Tempo.TestAutomation.Model.Web.Components.PageContainers
{
    public class SidebarMenuList : IPageElementContainer
    {
        protected IWebElement ParentElement;

        public SidebarMenuList(IWebElement parentElement)
        {
            ParentElement = parentElement;
        }

        public IWebElement GetElementByText(string referenceText)
        {
            referenceText = referenceText.ToLower().Trim();
            return ParentElement.GetElements(By.CssSelector("a"))
                .Where(e => e.Text.ToLower().Trim().Contains(referenceText))
                .First();
        }
    }
}
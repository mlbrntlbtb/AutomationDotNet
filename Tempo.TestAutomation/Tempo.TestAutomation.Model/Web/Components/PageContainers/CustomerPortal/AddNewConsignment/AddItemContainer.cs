using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using Tempo.TestAutomation.Model.Web.Locators.Containers.CustomerPortal.AddNewConsignment;

namespace Tempo.TestAutomation.Model.Web.Components.PageContainers.CustomerPortal.AddNewConsignment
{
    public class AddItemContainer
    {
        protected IWebElement parentElement;

        public AddItemContainer(IWebElement parentElement)
        {
            this.parentElement = parentElement;
        }

        public IWebElement GetAddItemFormByIndex(int index)
        {
            if (index < 0)
                throw new Exception("Index for Add Item forms should be greater than 0");

            index = index == 0 ? index : index - 1;
            return parentElement.GetElements(AddItemContainerLocators.FormItems).ToList().ElementAt(index);
        }
    }
}
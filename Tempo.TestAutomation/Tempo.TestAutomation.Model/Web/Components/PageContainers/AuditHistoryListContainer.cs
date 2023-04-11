using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using Tempo.TestAutomation.Model.Interface;
using Tempo.TestAutomation.Model.Web.Locators.Containers;

namespace Tempo.TestAutomation.Model.Web
{
    public class AuditHistoryListContainer : IPageElementContainer
    {
        protected IWebElement ParentElement;

        public AuditHistoryListContainer(IWebElement ParentElement)
        {
            this.ParentElement = ParentElement;
        }

        public IWebElement GetElementByText(string referenceText)
        {
            referenceText = referenceText.ToLower().Trim();
            return GetListItems().Where(x => x.Text.ToLower().Trim().Contains(referenceText))
                .First();
        }

        public IWebElement GetListItem(int itemIndex)
        {
            return GetListItems().ElementAt(itemIndex);
        }

        public string GetListItemActivityValue(int itemIndex)
        {
            return GetListItem(itemIndex).GetElement(AuditHistoryListContainerLocator.Label.Activity).Text.Trim();
        }

        public string GetListItemConfigurationSettingValue(int itemIndex)
        {
            return GetListItem(itemIndex).GetElement(AuditHistoryListContainerLocator.Label.ConfigurationSetting).Text.Trim();
        }

        public string GetListItemDateValue(int itemIndex)
        {
            return GetListItem(itemIndex).GetElement(AuditHistoryListContainerLocator.Label.Date).Text.Trim();
        }

        public IList<IWebElement> GetListItems()
        {
            return ParentElement.GetElements(AuditHistoryListContainerLocator.AuditHistoryCard);
        }

        public AuditHistoryListContainer Load() => this;
    }
}
using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using Tempo.TestAutomation.Model.Web.Components.Object;
using Tempo.TestAutomation.Model.Web.Components.PageContainers;
using Tempo.TestAutomation.Model.Web.Locators.Modals;

namespace Tempo.TestAutomation.Model.Web.Components.Modals
{
    public class AuditHistoryModal : TempoBasePage<AuditHistoryModal>, ILoadable<AuditHistoryModal>
    {
        private readonly IWebDriver driver;
        private readonly DropDownListContainer dropDownList;
        private readonly LoadingWheel loadingWheel;

        public AuditHistoryModal(IWebDriver driver, DropDownListContainer dropDownList, LoadingWheel loadingWheel) : base(driver)
        {
            this.driver = driver;
            this.dropDownList = dropDownList;
            this.loadingWheel = loadingWheel;
        }

        public string GetAuditHistoryCardActivity(int itemIndex)
        {
            var auditHistoryListContainer = new AuditHistoryListContainer(driver.GetElement(AuditHistoryModalLocators.List.AuditHistory));
            return auditHistoryListContainer.GetListItemActivityValue(itemIndex);
        }

        public string GetAuditHistoryCardConfigurationSetting(int itemIndex)
        {
            var auditHistoryListContainer = new AuditHistoryListContainer(driver.GetElement(AuditHistoryModalLocators.List.AuditHistory));
            return auditHistoryListContainer.GetListItemConfigurationSettingValue(itemIndex);
        }

        public string GetAuditHistoryCardDate(int itemIndex)
        {
            var auditHistoryListContainer = new AuditHistoryListContainer(driver.GetElement(AuditHistoryModalLocators.List.AuditHistory));
            return auditHistoryListContainer.GetListItemDateValue(itemIndex);
        }

        public void SelectSortOrder(string referenceValue)
        {
            driver.GetElement(AuditHistoryModalLocators.Button.Sort).Click();
            dropDownList.SelectByText(referenceValue);
        }

        protected override bool EvaluateLoadedStatus()
        {
            loadingWheel.WaitToDisappear();
            bool IsAuditHistoryHeaderPresent = driver.IsElementPresent(AuditHistoryModalLocators.Label.Header);
            return IsAuditHistoryHeaderPresent;
        }
    }
}
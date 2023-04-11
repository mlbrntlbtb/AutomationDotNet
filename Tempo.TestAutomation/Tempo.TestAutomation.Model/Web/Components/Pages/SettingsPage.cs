using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using Tempo.TestAutomation.Model.Web.Components.Common;
using Tempo.TestAutomation.Model.Web.Components.Elements;
using Tempo.TestAutomation.Model.Web.Components.Object;
using Tempo.TestAutomation.Model.Web.Components.PageContainers;
using Tempo.TestAutomation.Model.Web.Locators.Pages;

namespace Tempo.TestAutomation.Model.Web.Components.Pages
{
    public class SettingsPage : TempoBasePage<SettingsPage>, ILoadable<SettingsPage>
    {
        private readonly IWebDriver driver;
        private readonly DropDownListContainer dropDownList;
        private readonly LoadingWheel loadingWheel;
        private readonly ToastMessage toastMessage;

        public SettingsPage(IWebDriver driver, DropDownListContainer dropDownList, LoadingWheel loadingWheel, ToastMessage toastMessage)
            : base(driver)
        {
            this.driver = driver;
            this.dropDownList = dropDownList;
            this.loadingWheel = loadingWheel;
            this.toastMessage = toastMessage;
        }

        public void ClickEditButton()
        {
            driver.GetElement(SettingsPageLocators.Button.Edit).Click();
        }

        public void ClickTableRow(int rowIndex)
        {
            Table SettingsTable = new Table(driver.GetElement(SettingsPageLocators.Table.Settings), driver);
            SettingsTable.ClickRow(rowIndex);
        }

        public void CloseToastMessage()
        {
            toastMessage.CloseToastMessage();
        }

        public int? GetRowIndex(string columnName, string referenceText)
        {
            Table SettingsTable = new Table(driver.GetElement(SettingsPageLocators.Table.Settings), driver);
            int? rowIndex = SettingsTable.GetRowIndexContainingText(columnName, referenceText);
            return rowIndex;
        }

        public string GetToastMessage()
        {
            return toastMessage.GetToastMessageValue();
        }

        public bool IsRowSelected(int rowIndex)
        {
            Table SettingsTable = new Table(driver.GetElement(SettingsPageLocators.Table.Settings), driver);
            bool isRowSelected = SettingsTable.IsRowHighlighted(rowIndex);
            return isRowSelected;
        }

        public void SelectMenuItem(string referenceItem)
        {
            driver.GetElement(SettingsPageLocators.Button.MenuList).Click();
            dropDownList.SelectByText(referenceItem);
        }
        protected override bool EvaluateLoadedStatus()
        {
            loadingWheel.WaitToDisappear();
            bool IsHeaderDisplayed = driver.IsElementPresent(SettingsPageLocators.Label.Header);
            return IsHeaderDisplayed;
        }
    }
}
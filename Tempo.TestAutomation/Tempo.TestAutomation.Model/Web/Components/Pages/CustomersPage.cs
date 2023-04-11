using Datacom.TestAutomation.Common;
using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using Tempo.TestAutomation.Model.DTOs;
using Tempo.TestAutomation.Model.Web.Components.Common;
using Tempo.TestAutomation.Model.Web.Components.Object;
using Tempo.TestAutomation.Model.Web.Components.PageContainers;
using Tempo.TestAutomation.Model.Web.Locators.Pages;

namespace Tempo.TestAutomation.Model.Web.Components.Pages
{
    public class CustomersPage : TempoBasePage<CustomersPage>, ILoadable<CustomersPage>
    {
        private readonly IWebDriver driver;
        private readonly DropDownListContainer dropDownList;
        private readonly LoadingWheel loadingWheel;

        public CustomersPage(IWebDriver driver, DropDownListContainer dropDownList, LoadingWheel loadingWheel)
            : base(driver)
        {
            this.driver = driver;
            this.dropDownList = dropDownList;
            this.loadingWheel = loadingWheel;
        }

        public void ClickAddCustomerButton()
        {
            driver.GetElement(CustomersPageLocators.CustomersFrame.NavBar.Button.AddCustomer).Click();
        }

        public void ClickClearLayoutButton()
        {
            driver.GetElement(CustomersPageLocators.CustomersFrame.NavBar.Button.ClearLayout).Click();
        }

        public void ClickCustomerRow(int rowIndex)
        {
            Table CustomersTable = new Table(driver.GetElement(CustomersPageLocators.CustomersFrame.Table.Customer), driver);
            CustomersTable.ClickRow(rowIndex);
        }

        public void ClickCustomerRowLink(string columnText, int rowIndex)
        {
            Table CustomersTable = new Table(driver.GetElement(CustomersPageLocators.CustomersFrame.Table.Customer), driver);
            CustomersTable.ClickCellLink(columnText, rowIndex);
        }

        public void ClickDetailsColumnSettingsButton()
        {
            driver.GetElement(CustomersPageLocators.CustomersFrame.Table.Button.DetailsColumnSettings).Click();
        }

        public void ClickEditCustomerButton()
        {
            driver.GetElement(CustomersPageLocators.CustomersFrame.NavBar.Button.EditCustomer).Click();
        }

        public void ClickFilterButton()
        {
            driver.GetElement(CustomersPageLocators.CustomersFrame.Table.Button.ColumnFilter).Click();
        }

        public void ClickSaveLayoutButton()
        {
            driver.GetElement(CustomersPageLocators.CustomersFrame.NavBar.Button.SaveLayout).Click();
        }

        public string GetCustomer(CustomersDetails customersDetails)
        {
            if (customersDetails.CustomerDetails!.Customer.IsNotNullOrEmpty())
            {
                return customersDetails.CustomerDetails.Customer!;
            }
            return String.Empty;
        }

        public void HoverFilterListItem()
        {
            dropDownList.MouseHoverByText("Filter");
        }

        public bool IsDropDownDisplayed()
        {
            return dropDownList.IsDropDownPresent();
        }

        public bool IsFilterFieldPopulatedByText(string textToVerify)
        {
            return driver.GetElement(CustomersPageLocators.CustomersFrame.Table.TextBox.ColumnSettingsFilter).GetAttribute("value").Equals(textToVerify);
        }

        public bool IsLayoutClearedMessageDisplayed()
        {
            loadingWheel.WaitToDisappear();
            return driver.GetElement(CustomersPageLocators.CustomersFrame.Label.NotificationMessage).Text == "Your configuration has been cleared.";
        }

        public bool IsLayoutSavedMessageDisplayed()
        {
            loadingWheel.WaitToDisappear();
            return driver.GetElement(CustomersPageLocators.CustomersFrame.Label.NotificationMessage).Text == "Your configuration has been saved.";
        }

        public bool IsSettingsDropDownSubmenuDisplayed()
        {
            return dropDownList.IsDropDownItemPresent("Show items with value that:");
        }

        public bool IsTableFiltered()
        {
            loadingWheel.WaitToDisappear();
            return driver.GetElement(CustomersPageLocators.CustomersFrame.Table.Button.DetailsColumnSettings).GetAttribute("class").Contains("k-state-active");
        }

        public bool IsTableFilteredByName(string referenceText, string columnName)
        {
            loadingWheel.WaitToDisappear();
            Table CustomersTable = new Table(driver.GetElement(CustomersPageLocators.CustomersFrame.Table.Customer), driver);
            return CustomersTable.IsColumnValueConsistent(referenceText, columnName);
        }

        public void SetFilterValue(string filterValue)
        {
            driver.GetElement(CustomersPageLocators.CustomersFrame.Table.TextBox.ColumnSettingsFilter).SendKeys(filterValue);
        }

        protected override bool EvaluateLoadedStatus()
        {
            loadingWheel.WaitToDisappear();

            bool IsAddCustomerPresent = driver.IsElementPresent(CustomersPageLocators.CustomersFrame.NavBar.Button.AddCustomer);
            bool IsCustomerTablePresent = driver.IsElementPresent(CustomersPageLocators.CustomersFrame.Table.Customer);
            bool IsCustomerTableEnabled = driver.GetElement(CustomersPageLocators.CustomersFrame.Table.Customer).Enabled;

            return IsAddCustomerPresent && IsCustomerTablePresent && IsCustomerTableEnabled;
        }
    }
}
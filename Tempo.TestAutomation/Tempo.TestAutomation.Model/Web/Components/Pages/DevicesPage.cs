using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using Tempo.TestAutomation.Model.Web.Components.Common;
using Tempo.TestAutomation.Model.Web.Components.Object;
using Tempo.TestAutomation.Model.Web.Components.PageContainers;
using Tempo.TestAutomation.Model.Web.Locators.Pages;

namespace Tempo.TestAutomation.Model.Web.Components.Pages
{
    public class DevicesPage : TempoBasePage<DevicesPage>, ILoadable<DevicesPage>
    {
        private readonly IWebDriver driver;
        private readonly DropDownListContainer dropDownList;
        private readonly LoadingWheel loadingWheel;

        public DevicesPage(IWebDriver driver, DropDownListContainer dropDownList, LoadingWheel loadingWheel)
            : base(driver)
        {
            this.driver = driver;
            this.dropDownList = dropDownList;
            this.loadingWheel = loadingWheel;
        }

        public void ClickApply()
        {
            driver.GetElement(DevicesPageLocators.DeviceLogsFrame.Button.Apply).Click();
            loadingWheel.WaitToDisappear();
        }

        public void ClickDevicesRow(int rowToClick = 0)
        {
            Table DevicesTable = new Table(driver.GetElement(DevicesPageLocators.DevicesFrame.Table.Devices), driver);
            DevicesTable.ClickRow(rowToClick);
        }

        public void ClickDevicesRowByText(string referenceText)
        {
            Table DevicesTable = new Table(driver.GetElement(DevicesPageLocators.DevicesFrame.Table.Devices), driver);
            DevicesTable.ClickRow(Convert.ToInt32(DevicesTable.GetRowIndex("Serial Number", referenceText)));
        }

        public void ClickDisable()
        {
            driver.GetElement(DevicesPageLocators.DevicesFrame.Table.Button.Disable).Click();
            loadingWheel.WaitToDisappear();
        }

        public void ClickEnable()
        {
            driver.GetElement(DevicesPageLocators.DevicesFrame.Table.Button.Enable).Click();
            loadingWheel.WaitToDisappear();
        }

        public int GetRowIndexByText(string referenceText, string columnName)
        {
            Table DevicesTable = new Table(driver.GetElement(DevicesPageLocators.DevicesFrame.Table.Devices), driver);

            return Convert.ToInt32(DevicesTable.GetRowIndex(columnName, referenceText));
        }

        public String GetRowSerial(int rowToClick = 0)
        {
            Table DevicesTable = new Table(driver.GetElement(DevicesPageLocators.DevicesFrame.Table.Devices), driver);

            return DevicesTable.GetCellValue("Serial Number", 0);
        }

        public bool IsCellWithText(int rowIndex, string columnName)
        {
            Table DevicesTable = new Table(driver.GetElement(DevicesPageLocators.DevicesFrame.Table.Devices), driver);

            return DevicesTable.IsCellWithText(columnName, rowIndex);
        }

        public bool IsDateQuickSelectItemSelected(string referenceText)
        {
            referenceText = referenceText.ToLower().Trim();
            IWebElement ParentElement = driver.GetElement(DevicesPageLocators.DeviceLogsFrame.Button.DateQuickSelectMenu);

            return ParentElement.Text.ToLower().Trim().Equals(referenceText);
        }

        public bool IsNoLogsMessageDisplayed()
        {
            IWebElement ParentElement = driver.GetElement(DevicesPageLocators.DeviceLogsFrame.Label.LogMessage);

            return ParentElement.Text.Equals("- THERE ARE NO LOGS AVAILABLE FOR THIS DEVICE");
        }

        public bool IsRowHighlighted(int rowIndex = 0)
        {
            Table DevicesTable = new Table(driver.GetElement(DevicesPageLocators.DevicesFrame.Table.Devices), driver);

            return DevicesTable.IsRowHighlighted(rowIndex);
        }

        public bool IsRowWithTextHighlighted(string referenceText, string columnName)
        {
            Table DevicesTable = new Table(driver.GetElement(DevicesPageLocators.DevicesFrame.Table.Devices), driver);

            return DevicesTable.IsRowHighlighted(Convert.ToInt32(DevicesTable.GetRowIndex(columnName, referenceText)));
        }

        public bool IsSerialNumberEqual(string referenceText, int rowIndex = 0)
        {
            referenceText = referenceText.ToLower().Trim();
            Table DevicesTable = new Table(driver.GetElement(DevicesPageLocators.DevicesFrame.Table.Devices), driver);

            return DevicesTable.GetCellValue("Serial Number", rowIndex).ToLower().Trim().Equals(referenceText);
        }
        public void SelectCurrentDate()
        {
            driver.GetElement(DevicesPageLocators.DeviceLogsFrame.Button.DateQuickSelectMenu).Click();
            dropDownList.SelectByText("Today");
        }

        protected override bool EvaluateLoadedStatus()
        {
            loadingWheel.WaitToDisappear();
            bool IsDateQuickSelectPresent = driver.IsElementPresent(DevicesPageLocators.DeviceLogsFrame.Button.DateQuickSelectMenu);
            bool IsDevicesTablePresent = driver.IsElementPresent(DevicesPageLocators.DevicesFrame.Table.Devices);

            return IsDateQuickSelectPresent && IsDevicesTablePresent;
        }
    }
}
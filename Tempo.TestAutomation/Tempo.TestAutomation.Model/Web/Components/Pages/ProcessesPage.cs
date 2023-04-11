using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using Tempo.TestAutomation.Model.Web.Components.Common;
using Tempo.TestAutomation.Model.Web.Components.Elements;
using Tempo.TestAutomation.Model.Web.Components.Object;
using Tempo.TestAutomation.Model.Web.Locators.Pages;

namespace Tempo.TestAutomation.Model.Web.Components.Pages
{
    public class ProcessesPage : TempoBasePage<ProcessesPage>, ILoadable<ProcessesPage>
    {
        private readonly IWebDriver driver;
        private readonly LoadingWheel loadingWheel;
        private readonly TableFilter tableFilter;

        public ProcessesPage(IWebDriver driver, LoadingWheel loadingWheel, TableFilter tableFilter)
            : base(driver)
        {
            this.driver = driver;
            this.loadingWheel = loadingWheel;
            this.tableFilter = tableFilter;
        }

        public void ClickColumnFilterSettings(string TableColumnHeadingSettings)
        {
            tableFilter.ClickonColumnSettings(TableColumnHeadingSettings);
        }

        public bool FilterButtonDisplayed()
        {
            return tableFilter.IsFilterButtonDisplayed();
        }

        public void HoverOnFilter()
        {
            tableFilter.HoverOverFilter();
        }

        public void EnterFilterText(String text)
        {
            tableFilter.SendTextToFilter("Value", text);
        }

        public void ClickOnFilterButton()
        {
            tableFilter.FilterButton();
        }

        public string VerifyTableRecordDelivery()
        {
            loadingWheel.WaitToDisappear();
            Table ProcessTable = new Table(driver.GetElement(ProcessesPageLocators.ProcessFrame.Table.DeliveryRecord), driver);
            var VerifyDeliveryReocrd = ProcessTable.GetCellValue("Short Display Name", 0);
            return VerifyDeliveryReocrd;
        }

        public void ClickOnDeliveryRecord(int row)
        {
            loadingWheel.WaitToDisappear();
            Table ProcessTable = new Table(driver.GetElement(ProcessesPageLocators.ProcessFrame.Table.DeliveryRecord), driver);
            ProcessTable.ClickRow(row);
        }

        public void ClickEditProcessButton()
        {
            driver.GetElement(ProcessesPageLocators.ProcessFrame.NavBar.Button.EditProcess).Click();
        }

        protected override bool EvaluateLoadedStatus()
        {
            loadingWheel.WaitToDisappear();
            bool IsDashboardPresent = driver.IsElementPresent(ProcessesPageLocators.ProcessFrame.Table.ColumnHeader);

            return IsDashboardPresent;
        }
    }
}
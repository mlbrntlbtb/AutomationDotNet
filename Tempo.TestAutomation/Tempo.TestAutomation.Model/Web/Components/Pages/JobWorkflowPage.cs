using Datacom.TestAutomation.Common;
using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Tempo.TestAutomation.Model.DTOs;
using Tempo.TestAutomation.Model.Web.Components.Common;
using Tempo.TestAutomation.Model.Web.Components.Elements;
using Tempo.TestAutomation.Model.Web.Components.Object;
using Tempo.TestAutomation.Model.Web.Components.PageContainers;
using Tempo.TestAutomation.Model.Web.Locators.Pages;

namespace Tempo.TestAutomation.Model.Web.Components.Pages
{
    public class JobWorkflowPage : TempoBasePage<JobWorkflowPage>, ILoadable<JobWorkflowPage>
    {
        private readonly IWebDriver driver;
        private readonly DropDownListContainer dropDownList;
        private readonly LoadingWheel loadingWheel;
        private readonly TableFilter tableFilter;

        public JobWorkflowPage(IWebDriver driver, DropDownListContainer dropDownList, LoadingWheel loadingWheel, TableFilter tableFilter)
            : base(driver)
        {
            this.driver = driver;
            this.dropDownList = dropDownList;
            this.loadingWheel = loadingWheel;
            this.tableFilter = tableFilter;
        }

        public string GetJobWorkFlow(JobWorkflowDetails jobWorkFlowDetails)
        {
            if (jobWorkFlowDetails.JobworkflowDetails!.ShortName.IsNotNullOrEmpty())
            {
                return jobWorkFlowDetails.JobworkflowDetails.ShortName!;
            }
            return String.Empty;
        }

        public void SetFilterValue(string filterValue)
        {
            driver.GetElement(JobWorkflowPageLocators.JobWorkflowFrame.FirstTextBox.ColumnSettingsFilter).SendKeys(filterValue);
        }

        public bool IsFilterFieldPopulated(string textToVerify)
        {
            return driver.GetElement(CustomersPageLocators.CustomersFrame.Table.TextBox.ColumnSettingsFilter).GetAttribute("value").Equals(textToVerify);
        }

        public void ClickShortNameButton()
        {
            tableFilter.ClickonColumnSettings("Short Name");
        }

        public bool IsDropDownListDisplayed()
        {
            return dropDownList.IsDropDownPresent();
        }

        public void ClickFilterListItem()
        {
            dropDownList.MouseHoverByText("Filter");
        }

        public bool IsShortNameDropDownSubmenuDisplayed()
        {
            return dropDownList.IsDropDownItemPresent("Show items with value that:");
        }

        public void ClickFilterButton()
        {
            driver.GetElement(CustomersPageLocators.CustomersFrame.Table.Button.ColumnFilter).Click();
        }

        public bool IsShortNameFiltered()
        {
            loadingWheel.WaitToDisappear();
            return driver.GetElement(JobWorkflowPageLocators.JobWorkflowFrame.Table.ColumnSettingsDetails).GetAttribute("class").Contains("k-state-active");
        }

        public bool IsTableFilteredByShortName(string referenceText, string columnName)
        {
            loadingWheel.WaitToDisappear();
            Table JobWorkflowTable = new Table(driver.GetElement(JobWorkflowPageLocators.JobWorkflowFrame.Table.JobWorkflow), driver);
            return JobWorkflowTable.IsFilteredValueConsistent(referenceText, columnName);
        }

        public void ClickJobWorkFlowRow(int rowToClick = 0)
        {
            Table JobWorkFlowTable = new Table(driver.GetElement(JobWorkflowPageLocators.JobWorkflowFrame.Table.JobWorkflow), driver);
            JobWorkFlowTable.ClickRow(rowToClick);
        }

        public bool IsFirstRowHighlighted(int rowIndex = 0)
        {
            Table JobWorkflowTable = new Table(driver.GetElement(JobWorkflowPageLocators.JobWorkflowFrame.Table.JobWorkflow), driver);

            return JobWorkflowTable.IsRowHighlighted(rowIndex);
        }

        public void ClickEditButton()
        {
            driver.GetElement(JobWorkflowPageLocators.JobWorkflowFrame.Button.Edit).Click();
        }

        public void ClickEditWorkflowButton()
        {
            driver.GetElement(JobWorkflowPageLocators.JobWorkflowFrame.Button.EditWorkflow).Click();
        }

        public bool IsPopupDisplayed()
        {
            loadingWheel.WaitToDisappear();
            return driver.GetElement(JobWorkflowPageLocators.JobWorkflowFrame.Label.PopUp).Text == "UPDATE EXISTING JOB WORKFLOW";
        }

        public void ClickWorkflowButton()
        {
            driver.GetElement(JobWorkflowPageLocators.JobWorkflowFrame.Button.Workflow).Click();
        }

        public bool IsWorkflowPageDisplayed()
        {
            return driver.GetElement(JobWorkflowPageLocators.JobWorkflowFrame.Label.Delivery).Text == "Delivery";
        }

        public bool IsAdditionalFieldsDisplayed()
        {
            return driver.GetElement(JobWorkflowPageLocators.JobWorkflowFrame.Label.Process).Text == "Process";
        }

        public void ClickCanCreateCheckbox()
        {
            if (driver.GetElement(JobWorkflowPageLocators.JobWorkflowFrame.Button.CanCreate).Selected == false)
                driver.GetElement(JobWorkflowPageLocators.JobWorkflowFrame.Button.CanCreate).Click();
        }

        public bool IsCanCreateCheckboxDisabled()
        {
            return driver.GetElement(JobWorkflowPageLocators.JobWorkflowFrame.Label.Process).Text == "Process";
        }

        public void ClickSaveButton()
        {
            Actions Save = new(driver);
            Save.MoveToElement(driver.GetElement(JobWorkflowPageLocators.JobWorkflowFrame.Button.Save)).Perform();
            Save.Click();
            Save.Build().Perform();
            loadingWheel.WaitToDisappear();
        }

        protected override bool EvaluateLoadedStatus()
        {
            loadingWheel.WaitToDisappear();
            bool IsJobWorkflowTablePresent = driver.IsElementPresent(JobWorkflowPageLocators.JobWorkflowFrame.Table.JobWorkflow);

            return IsJobWorkflowTablePresent;
        }
    }
}
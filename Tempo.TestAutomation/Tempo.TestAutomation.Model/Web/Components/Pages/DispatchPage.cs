using Datacom.TestAutomation.Common;
using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using ServiceStack;
using Tempo.TestAutomation.Model.DTOs;
using Tempo.TestAutomation.Model.Web.Components.Common;
using Tempo.TestAutomation.Model.Web.Components.Elements;
using Tempo.TestAutomation.Model.Web.Components.Object;
using Tempo.TestAutomation.Model.Web.Components.PageContainers;
using Tempo.TestAutomation.Model.Web.Locators.Page;
using Tempo.TestAutomation.Model.Web.Locators.Pages;
using FileExtensions = Datacom.TestAutomation.Common.Extensions.FileExtensions;

namespace Tempo.TestAutomation.Model.Web.Components.Pages
{
    public class DispatchPage : TempoBasePage<DispatchPage>, ILoadable<DispatchPage>
    {
        private readonly IWebDriver driver;
        private readonly DropDownListContainer dropDownList;
        private readonly LoadingProgressBar loadingProgressBar;
        private readonly LoadingWheel loadingWheel;
        private readonly ToastMessage toastMessage;
        private bool isJobExisting;

        public DispatchPage(IWebDriver driver, DropDownListContainer dropDownList, LoadingWheel loadingWheel, ToastMessage toastMessage, LoadingProgressBar loadingProgressBar)
            : base(driver)
        {
            this.driver = driver;
            this.dropDownList = dropDownList;
            this.loadingWheel = loadingWheel;
            this.toastMessage = toastMessage;
            this.loadingProgressBar = loadingProgressBar;
            isJobExisting = false;
        }

        public void AddConsignment()
        {
            driver.GetElement(DispatchPageLocators.JobsFrame.NavBar.Button.DispatchMenu).Click();
            dropDownList.SelectByText("Add Consignment");
        }

        public void AssignJobToRoute(string from, string to)
        {
            Actions builder = new Actions(driver);
            builder.DragAndDrop(driver.GetElement(By.CssSelector("#dispatchJobGrid tr:nth-child(" + from + ") td:nth-child(9)")), driver.GetElement(By.XPath("//span[contains(text(),'" + to + "')]"))).Perform();
            loadingWheel.WaitToDisappear();
        }

        public void AssignMultipleJobToRoute(int NumberOfRow, string to)
        {
            Actions builder = new Actions(driver);
            IWebElement JobsTable = driver.GetElement(DispatchPageLocators.JobsFrame.Table.DispatchJob);
            IList<IWebElement> Rows = (IList<IWebElement>)JobsTable.GetElements(DispatchPageLocators.JobsFrame.Table.TableRow);
            for (int i = 0; i < NumberOfRow; i++)
            {
                builder.KeyDown(Keys.Shift).Click(Rows[i]).Perform();
            }
            builder.DragAndDrop(Rows[0], driver.GetElement(By.XPath("//span[contains(text(),'" + to + "')]"))).Perform();
            loadingWheel.WaitToDisappear();
        }

        public void CancelAllocation()
        {
            driver.GetElement(DispatchPageLocators.JobsFrame.NavBar.Button.DispatchMenu).Click();
            dropDownList.SelectByText("Cancel Allocations");
        }

        public void CancelJob()
        {
            driver.GetElement(DispatchPageLocators.JobsFrame.NavBar.Button.DispatchMenu).Click();
            dropDownList.SelectByText("Cancel Jobs");
        }

        public void CancelRouteAllocations()
        {
            if (driver.GetElement(By.CssSelector("div[class=\"active_count ng-binding\"]")).GetTextContent() != " 0 ")
            {
                driver.GetElement(DispatchPageLocators.RouteFrame.Button.RouteOption).Click();
                driver.GetElement(DispatchPageLocators.RouteFrame.Button.CancelAllocations).Click();
                driver.GetElement(DispatchPageLocators.RouteFrame.Button.Yes).Click();
                driver.WaitUntilElementIsNotPresent(DispatchPageLocators.Dialog.Progress);
            }
        }

        public bool CheckFilteredAvailableItemsByJobStatus(string jobstatus)
        {
            var dispatchRoute = new DispatchRouteContainer(driver.GetElement(DispatchRouteContainerLocators.RouteDetail.Container));
            try
            {
                TimeSpan maxWaitTime = TimeSpan.FromSeconds(2);
                List<IWebElement> dispatchRouteDetailCards = dispatchRoute.GetRouteDetailCards(maxWaitTime);
                return dispatchRouteDetailCards.Where(r => r.IsElementTextContains(jobstatus)).Any();
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void ClickAddJob()
        {
            driver.GetElement(DispatchPageLocators.JobsFrame.NavBar.Button.AddJob).Click();
        }

        public void ClickAutoAdhoc()
        {
            driver.GetElement(DispatchPageLocators.RouteFrame.ListBox.ProcessJob).Click();
            driver.GetElement(DispatchPageLocators.RouteFrame.ListBox.AutoAdhoc).Click();
        }

        public void ClickBranchDropdownField()
        {
            driver.GetElement(DispatchPageLocators.JobsFrame.NavBar.DropdownField.Branches).Click();
        }

        public void ClickCreatedJobRow(DispatchJobDetails dispatchJobDetails, int rowToCheck = 0)
        {
            Table JobsTable = new Table(driver.GetElement(DispatchPageLocators.JobsFrame.Table.DispatchJob), driver);
            int createdJobRowIndex = (int)JobsTable.GetRowIndex(dispatchJobDetails.JobDetails!.ColumnName!, dispatchJobDetails.JobDetails!.SpecialInstruction!, rowToCheck)!;
            if (createdJobRowIndex == -1)
                isJobExisting = false;
            else
            {
                JobsTable.ClickCell(dispatchJobDetails.JobDetails!.ColumnName!, createdJobRowIndex);
                isJobExisting = true;
            }
        }

        public void ClickDispatchTableFirstRow(int rowToClick = 0, string columnName = "Status")
        {
            loadingWheel.WaitToDisappear();
            Table DispatchTable = new Table(driver.GetElement(DispatchPageLocators.JobsFrame.Table.DispatchJob), driver);
            DispatchTable.ClickCell(columnName, rowToClick);
        }

        public void ClickEditWorkflowButton()
        {
            driver.GetElement(DispatchPageLocators.JobsFrame.NavBar.Button.Edit).Click();
        }

        public void ClickHamburgerButton()
        {
            driver.GetElement(DispatchPageLocators.JobsFrame.NavBar.List.MenuButton).Click();
        }

        public void ClickJobStatus(string jobstatus)
        {
            driver.GetElement(DispatchPageLocators.RouteFrame.NavBar.Button.JobStatusFilterDropDown).Click();
            var JobStatus = new DropDownListContainer(driver);
            JobStatus.SelectByExactText(jobstatus);
            driver.GetElement(DispatchPageLocators.RouteFrame.NavBar.Button.JobStatusFilterDropDown).Click();
        }

        public void ClickJobStatusFilterDropDown()
        {
            driver.GetElement(DispatchPageLocators.RouteFrame.NavBar.Button.JobStatusFilterDropDown).Click();
        }

        public bool ClickOnRandomElement()
        {
            if (driver.GetElement(HomePageLocators.NavBar.Text.AuthorisedUser).Displayed)
            {
                driver.GetElement(HomePageLocators.NavBar.Text.AuthorisedUser).Click();
            }
            return true;
        }

        public void ClickPrintJob()
        {
            driver.GetElement(DispatchPageLocators.JobsFrame.NavBar.Button.PrintJob).Click();
        }

        public void ClickProcessJob()
        {
            driver.GetElement(DispatchPageLocators.RouteFrame.ListBox.JobProcess).Click();
            loadingWheel.WaitToDisappear();
        }

        public void ClickRouteOptions()
        {
            driver.GetElement(DispatchPageLocators.RouteFrame.Button.routeOptions).Click();
        }

        public void ClickRouteResult(string route)
        {
            var clickroute = new DispatchRouteContainer(driver.GetElement(DispatchPageLocators.RouteFrame.ListBox.dispatchRoute));

            clickroute.GetElementByText(route).JavaScriptClick();
        }

        public void ClickRow(int rowIndex)
        {
            Table JobsTable = new Table(driver.GetElement(DispatchPageLocators.JobsFrame.Table.DispatchJob), driver);
            JobsTable.ClickRow(rowIndex);
        }

        public void ClickViewRouteMap()
        {
            driver.GetElement(DispatchPageLocators.RouteFrame.Button.viewRouteMap).Click();
        }

        public void CloseToastMessage()
        {
            driver.GetElement(DispatchPageLocators.Toast.Close).Click();
            driver.WaitUntilElementIsNotPresent(DispatchPageLocators.Toast.Text);
        }

        public void FillConfirmation(DispatchJobDetails dispatchJobDetails)
        {
            driver.GetElement(DispatchPageLocators.RouteFrame.ListBox.ReasonForChange).Click();
            driver.GetElement(DispatchPageLocators.RouteFrame.ListBox.OnBehalfOfDriver).Click();
            driver.GetElement(DispatchPageLocators.RouteFrame.ListBox.Comment).SendKeys(dispatchJobDetails.JobDetails.SpecialInstruction);
        }

        public void FilterRoute(string route)
        {
            driver.GetElement(DispatchPageLocators.RouteFrame.NavBar.TextBox.FindARoute).SendKeys(route);
            loadingWheel.WaitToDisappear();
        }

        public string GetRoute(RouteDetails routeDetails)
        {
            if (routeDetails.RouteDetail!.RouteName.IsNotNullOrEmpty())
            {
                return routeDetails.RouteDetail.RouteName!;
            }
            return String.Empty;
        }

        public string GetToastMessage()
        {
            return toastMessage.GetToastMessageValue();
        }

        public bool IsAddConsignmentDisplayed()
        {
            loadingWheel.WaitToDisappear();
            driver.SwitchToLastWindow();
            return driver.IsElementPresent(ConsignmentPageLocators.Table.ConsignmentEquipment) && driver.IsPageTitle("New Consignment | Tempo");
        }

        public bool IsAllocatedToastMessageDisplayed()
        {
            IWebElement ToastMessage = driver.GetElement(DispatchPageLocators.Toast.Text);
            return ToastMessage.Text.Contains("allocated");
        }

        public void IsBranchesPresent(string referenceText1, string referenceText2)
        {
            driver.GetElements(DispatchPageLocators.JobsFrame.NavBar.DropdownField.Text.Branches)
                .Where(e => e.Text.ToLower().Trim().Contains(referenceText1));

            driver.GetElements(DispatchPageLocators.JobsFrame.NavBar.DropdownField.Text.Branches)
             .Where(e => e.Text.ToLower().Trim().Contains(referenceText2));
        }

        public bool IsConfirmationPopupDisplayed()
        {
            loadingWheel.WaitToDisappear();
            return driver.GetElement(DispatchPageLocators.RouteFrame.ListBox.Confirmation).Text == "CONFIRMATION";
        }

        public bool IsEditJobHeaderDisplayed()
        {
            loadingWheel.WaitToDisappear();
            return driver.GetElement(DispatchPageLocators.RouteFrame.ListBox.EditPopUp).Text == "WORKFLOW: AUTO";
        }

        public bool IsFirstRowHighlighted(int rowIndex = 0)
        {
            Table DispatchTable = new Table(driver.GetElement(DispatchPageLocators.JobsFrame.Table.DispatchJob), driver);

            return DispatchTable.IsRowHighlighted(rowIndex);
        }

        public bool IsJobDetailsFileValidComplete(string path, DispatchJobDetails DispatchJobData)
        {
            int maxWait = 20;
            string? fileName = DispatchJobData.FileName! + DispatchJobData.FileExtension!;
            loadingProgressBar.WaitToDisappear();
            return FileExtensions.WaitFileExists(maxWait, fileName, path);
        }

        public bool IsJobExisting()
        {
            loadingWheel.WaitToDisappear();
            return isJobExisting;
        }


        public bool IsJobStatusDropDownDisplayed()
        {
            return dropDownList.IsDropDownPresent();
        }

        public bool IsRoutePresent(string route)
        {
            var routeAvailability = new DispatchRouteContainer(driver.GetElement(DispatchPageLocators.RouteFrame.ListBox.dispatchRoute));
            return routeAvailability.GetElementByText(route).Displayed;
        }

        public void RestoreAutoUpdates()
        {
            driver.GetElement(DispatchPageLocators.JobsFrame.NavBar.Button.DispatchMenu).Click();
            dropDownList.SelectByText("Restart Auto Updates");
            driver.WaitUntilElementIsNotPresent(DispatchPageLocators.JobsFrame.NavBar.List.DispatchMenu);
        }

        public void SearchRoute(string route)
        {
            driver.GetElement(DispatchPageLocators.RouteFrame.NavBar.TextBox.FindARoute).SendKeys(route);
            loadingWheel.WaitToDisappear();
        }

        public void SelectDispatchMenuByText(string option)
        {
            driver.GetElement(DispatchPageLocators.JobsFrame.NavBar.Button.DispatchMenu).Click();
            dropDownList.SelectByText(option);
        }


        public void SuspendAutoUpdates(string option)
        {
            driver.GetElement(DispatchPageLocators.JobsFrame.NavBar.Button.DispatchMenu).Click();
            dropDownList.SelectByText(option);
            CloseToastMessage();
        }

        public void SwitchToAddConsigneeWindow()
        {
            driver.SwitchTo().Window(driver.WindowHandles[1]);
            loadingWheel.WaitToDisappear();
        }
        public void UnselectAllJobStatus()
        {
            dropDownList.SelectByExactText("Dispatched");
            dropDownList.SelectByExactText("Scheduled");
            dropDownList.SelectByExactText("Accepted");
            dropDownList.SelectByExactText("Completed");
        }

        public void WaitToDisappearCancelledMessage()
        {
            driver.WaitUntilElementIsNotPresent(DispatchPageLocators.JobsFrame.NavBar.Message.Cancel);
        }

        protected override bool EvaluateLoadedStatus()
        {
            loadingWheel.WaitToDisappear();
            bool IsAddJobPresent = driver.IsElementPresent(DispatchPageLocators.JobsFrame.NavBar.Button.AddJob);
            bool IsDispatchJobTablePresent = driver.IsElementPresent(DispatchPageLocators.JobsFrame.Table.DispatchJob);
            bool IsDispatchJobTableEnabled = driver.GetElement(DispatchPageLocators.JobsFrame.Table.DispatchJob).Enabled;
            bool IsDispatchRouteListPresent = driver.IsElementPresent(DispatchPageLocators.RouteFrame.ListBox.dispatchRoute);

            return IsAddJobPresent && IsDispatchJobTablePresent && IsDispatchRouteListPresent && IsDispatchJobTableEnabled;
        }
    }
}
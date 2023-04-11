using Datacom.TestAutomation.Common.Extensions;
using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using Tempo.TestAutomation.Model.DTOs;
using Tempo.TestAutomation.Model.DTOs.CustomerPortal.AddNewConsignment;
using Tempo.TestAutomation.Model.Utilities;
using Tempo.TestAutomation.Model.Web.Components.Common;
using Tempo.TestAutomation.Model.Web.Components.Elements;
using Tempo.TestAutomation.Model.Web.Components.Object;
using Tempo.TestAutomation.Model.Web.Components.PageContainers;
using Tempo.TestAutomation.Model.Web.Locators.Pages;

namespace Tempo.TestAutomation.Model.Web.Components.Pages
{
    public class EditOrdersPage : TempoBasePage<EditOrdersPage>, ILoadable<EditOrdersPage>
    {
        private readonly IWebDriver driver;
        private readonly DropDownListContainer dropDownList;
        private readonly LoadingWheel loadingWheel;
        private readonly LoadingProgressBar loadingProgressBar;
        private bool isJobExisting;

        public EditOrdersPage(IWebDriver driver, DropDownListContainer dropDownList, LoadingProgressBar loadingProgressBar, LoadingWheel loadingWheel)
            : base(driver)
        {
            this.driver = driver;
            this.loadingProgressBar = loadingProgressBar;
            this.dropDownList = dropDownList;
            this.loadingWheel = loadingWheel;
            isJobExisting = false;
        }

        public void ClickEditJobButton()
        {
            driver.GetElement(EditOrdersPageLocators.Body.Button.Edit).Click();
        }

        public void ClickJobRow(OrderData orderData, int rowToCheck = 0)
        {
            Table JobTable = new Table(driver.GetElement(EditOrdersPageLocators.Body.Table.Job), driver);
            int searchedJobRowIndex = (int)JobTable.GetRowIndex(orderData.JobColumnName!, orderData.Reference!, rowToCheck)!;
            if (searchedJobRowIndex == -1)
                isJobExisting = false;
            else
            {
                JobTable.ClickCell(orderData.JobColumnName!, searchedJobRowIndex);
                isJobExisting = true;
            }
        }

        public void ClickMapButton()
        {
            driver.GetElement(EditOrdersPageLocators.Body.Button.Map).Click();
        }

        public bool IsConsignmentNoteFileValidComplete(string path, AddNewConsignmentDetails addNewConsignmentDetails)
        {
            int countWait = 1;
            int maxWait = 10;
            loadingProgressBar.WaitToDisappear();
            while (countWait <= maxWait)
            {
                if (!FileExtensions.GetLatestFileName(path).Contains(addNewConsignmentDetails.FileName2!)
                & !FileExtensions.GetLatestFileExtension(path).Contains(addNewConsignmentDetails.FileExtension!))
                {
                    countWait++;
                    Thread.Sleep(1000);
                }
                else
                    return true;
            }
            return false;
        }

        public bool IsConsignmentNoteFileMatchData(string path, AddNewConsignmentDetails addNewConsignmentDetails)
        {
            string PDFContent = PDFUtilities.GetPDFText(path);
            return PDFContent.Contains(addNewConsignmentDetails.FileContentDetails!.Quantity!)
                & PDFContent.Contains(addNewConsignmentDetails.FileContentDetails!.Dimensions!)
                & PDFContent.Contains(addNewConsignmentDetails.FileContentDetails!.Values!);
        }

        public void SelectItemPrintMenu(string referenceItem)
        {
            driver.GetElement(EditOrdersPageLocators.Body.DropDownList.Print).Click();
            dropDownList.SelectByText(referenceItem);
        }

        protected override bool EvaluateLoadedStatus()
        {
            loadingWheel.WaitToDisappear();
            driver.WaitAvailability(EditOrdersPageLocators.Body.Header.EditOrder);
            bool IsEditOrderPagePresent = driver.IsElementPresent(EditOrdersPageLocators.Body.Header.EditOrder);

            return IsEditOrderPagePresent;
        }
    }
}
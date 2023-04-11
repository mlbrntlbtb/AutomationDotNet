using Datacom.TestAutomation.Common;
using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using ServiceStack;
using Tempo.TestAutomation.Model.DTOs;
using Tempo.TestAutomation.Model.Web.Components.Object;
using Tempo.TestAutomation.Model.Web.Components.PageContainers;
using Tempo.TestAutomation.Model.Web.Locators.Modal;

namespace Tempo.TestAutomation.Model.Web.Components.Modals
{
    public class AddJobModal : TempoBasePage<AddJobModal>, ILoadable<AddJobModal>
    {
        private readonly IWebDriver driver;
        private readonly DropDownListContainer dropDownList;
        private readonly LoadingWheel loadingWheel;

        public AddJobModal(IWebDriver driver, DropDownListContainer dropDownList, LoadingWheel loadingWheel)
            : base(driver)
        {
            this.driver = driver;
            this.dropDownList = dropDownList;
            this.loadingWheel = loadingWheel;
        }

        public void ClickAddSurchargeButton()
        {
            driver.GetElement(AddJobModalLocators.OrderPricingForm.Navbar.Button.AddSurcharge).Click();
            loadingWheel.WaitToDisappear();
        }

        public void ClickSaveButton()
        {
            driver.GetElement(AddJobModalLocators.Footer.Button.Save).Click();
            loadingWheel.WaitToDisappear();
        }

        public void CreateJobActionBranch(JobActionBranch jobActionBranch)
        {
            if (driver.GetElement(AddJobModalLocators.JobForm.Checkbox.CreateOrder).Selected == false)
                driver.GetElement(AddJobModalLocators.JobForm.Checkbox.CreateOrder).Click();

            if (jobActionBranch.Customer.IsNotNullOrEmpty())
            {
                driver.GetElement(AddJobModalLocators.JobForm.OrderForm.TextBox.Customer).SendKeys(jobActionBranch.Customer);
                dropDownList.SelectByText(jobActionBranch.Customer!);
            }

            if (jobActionBranch.Service.IsNotNullOrEmpty())
            {
                driver.GetElement(AddJobModalLocators.JobForm.OrderForm.TextBox.Service).Click();
                dropDownList.SelectByText(jobActionBranch.Service!);
            }

            if (jobActionBranch.JobDetails!.JobType.IsNotNullOrEmpty())
            {
                driver.GetElement(AddJobModalLocators.JobForm.Listbox.Jobtype).Click();
                dropDownList.SelectByText(jobActionBranch.JobDetails!.JobType!);
            }

            if (jobActionBranch.JobDetails!.ActionBranch.IsNotNullOrEmpty())
            {
                driver.GetElement(AddJobModalLocators.JobForm.TextBox.ActionBranch).SendKeys(jobActionBranch.JobDetails!.ActionBranch!);
                dropDownList.SelectByText(jobActionBranch.JobDetails!.ActionBranch!);
            }

            if (jobActionBranch.JobDetails!.ScheduledDateFormat.IsNotNullOrEmpty())
                driver.GetElement(AddJobModalLocators.JobForm.TextBox.ScheduledDate).SendKeys(DateUtilities.GetCurrentDateTime(jobActionBranch.JobDetails!.ScheduledDateFormat!));
        }

        public void CreateOrder(OrderData orderDetails)
        {
            if (driver.GetElement(AddJobModalLocators.JobForm.Checkbox.CreateOrder).Selected == false)
                driver.GetElement(AddJobModalLocators.JobForm.Checkbox.CreateOrder).Click();

            driver.GetElement(AddJobModalLocators.JobForm.OrderForm.TextBox.Customer).SendKeys(orderDetails.Customer);
            dropDownList.SelectByText(orderDetails.Customer!);

            driver.GetElement(AddJobModalLocators.JobForm.OrderForm.TextBox.Service).Click();
            dropDownList.SelectByText(orderDetails.Service!);
            loadingWheel.WaitToDisappear();

            if (orderDetails.Reference.IsNotNullOrEmpty())
                driver.GetElement(AddJobModalLocators.JobForm.OrderForm.TextBox.Reference).SendKeys(orderDetails.Reference);

            if (orderDetails.OrderInternalNotes.IsNotNullOrEmpty())
                driver.GetElement(AddJobModalLocators.JobForm.TextBox.OrderInternalNotes).SendKeys(orderDetails.OrderInternalNotes);
        }

        public void FillJobForm(DispatchJobDetails dispatchJobDetails, bool referenceWithTimeStamp = false)
        {
            if (dispatchJobDetails.JobDetails!.JobType.IsNotNullOrEmpty())
            {
                driver.GetElement(AddJobModalLocators.JobForm.Listbox.Jobtype).Click();
                dropDownList.SelectByText(dispatchJobDetails.JobDetails.JobType!);
            }

            if (dispatchJobDetails.JobDetails!.Route.IsNotNullOrEmpty())
            {
                driver.GetElement(AddJobModalLocators.JobForm.TextBox.Route).SendKeys(dispatchJobDetails.JobDetails.Route);
                dropDownList.SelectByText(dispatchJobDetails.JobDetails.Route!);
            }

            if (dispatchJobDetails.JobDetails!.Reference.IsNotNullOrEmpty())
            {
                if (referenceWithTimeStamp == true)
                    dispatchJobDetails.JobDetails.Reference = dispatchJobDetails.JobDetails.Reference + " " + DateUtilities.GetCurrentDateTime("yyyy-MM-dd HH:mm:ss").ToString();

                driver.GetElement(AddJobModalLocators.JobForm.TextBox.Reference).SendKeys(dispatchJobDetails.JobDetails.Reference);
            }

            if (dispatchJobDetails.JobDetails!.ItemCount.IsNotNullOrEmpty())
                driver.GetElement(AddJobModalLocators.JobForm.TextBox.ItemCount).SendKeys(dispatchJobDetails.JobDetails.ItemCount);

            if (dispatchJobDetails.JobDetails!.SpecialInstruction.IsNotNullOrEmpty())
                driver.GetElement(AddJobModalLocators.JobForm.TextBox.SpecialInstruction).SendKeys(dispatchJobDetails.JobDetails.SpecialInstruction);

            if (dispatchJobDetails.PickUpDetails!.Address.IsNotNullOrEmpty())
                driver.GetElement(AddJobModalLocators.PickUpDetailForm.TextBox.Address).SendKeys(dispatchJobDetails.PickUpDetails.Address);

            if (dispatchJobDetails.PickUpDetails!.EmailAddress.IsNotNullOrEmpty())
                driver.GetElement(AddJobModalLocators.PickUpDetailForm.TextBox.EmailAddress).SendKeys(dispatchJobDetails.PickUpDetails.EmailAddress);
        }

        public void FillAddJobForm(DispatchJobDetails dispatchJobDetails, bool referenceWithTimeStamp = false)
        {
            if (dispatchJobDetails.JobDetails!.JobType.IsNotNullOrEmpty())
            {
                driver.GetElement(AddJobModalLocators.JobForm.Listbox.Jobtype).Click();
                dropDownList.SelectByText(dispatchJobDetails.JobDetails.JobType!);
            }

            if (dispatchJobDetails.JobDetails!.Reference.IsNotNullOrEmpty())
            {
                if (referenceWithTimeStamp == true)
                    dispatchJobDetails.JobDetails.Reference = dispatchJobDetails.JobDetails.Reference + " " + DateUtilities.GetCurrentDateTime("yyyy-MM-dd HH:mm:ss").ToString();

                driver.GetElement(AddJobModalLocators.JobForm.TextBox.Reference).SendKeys(dispatchJobDetails.JobDetails.Reference);
            }

            if (dispatchJobDetails.JobDetails!.ItemCount.IsNotNullOrEmpty())
                driver.GetElement(AddJobModalLocators.JobForm.TextBox.ItemCount).SendKeys(dispatchJobDetails.JobDetails.ItemCount);

            if (dispatchJobDetails.JobDetails!.SpecialInstruction.IsNotNullOrEmpty())
                driver.GetElement(AddJobModalLocators.JobForm.TextBox.SpecialInstruction).SendKeys(dispatchJobDetails.JobDetails.SpecialInstruction);

            if (dispatchJobDetails.PickUpDetails!.Address.IsNotNullOrEmpty())
                driver.GetElement(AddJobModalLocators.PickUpDetailForm.TextBox.Address).SendKeys(dispatchJobDetails.PickUpDetails.Address);
            driver.GetElement(AddJobModalLocators.PickUpDetailForm.Dropdown.FirstAddress).Click();

            if (dispatchJobDetails.PickUpDetails!.EmailAddress.IsNotNullOrEmpty())
                driver.GetElement(AddJobModalLocators.PickUpDetailForm.TextBox.Suburb).SendKeys(dispatchJobDetails.PickUpDetails.Suburb);
        }

        protected override bool EvaluateLoadedStatus()
        {
            bool IsAddJobHeaderPresent = driver.IsElementPresent(AddJobModalLocators.Header.AddJob);
            return IsAddJobHeaderPresent;
        }
    }
}
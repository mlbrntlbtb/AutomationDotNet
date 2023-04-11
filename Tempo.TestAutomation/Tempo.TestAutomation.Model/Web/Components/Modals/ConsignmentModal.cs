using Datacom.TestAutomation.Common;
using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using Tempo.TestAutomation.Model.DTOs;
using Tempo.TestAutomation.Model.Web.Components.Object;
using Tempo.TestAutomation.Model.Web.Components.PageContainers;
using Tempo.TestAutomation.Model.Web.Locators.Modals;

namespace Tempo.TestAutomation.Model.Web.Components.Modals
{
    public class ConsignmentModal : TempoBasePage<ConsignmentModal>
    {
        private readonly IWebDriver driver;
        private readonly DropDownListContainer dropDownList;
        private readonly LoadingWheel loadingWheel;

        public ConsignmentModal(IWebDriver driver, DropDownListContainer dropDownList, LoadingWheel loadingWheel)
            : base(driver)
        {
            this.driver = driver;
            this.dropDownList = dropDownList;
            this.loadingWheel = loadingWheel;
        }

        public void AssignToRoute(ConsignmentData consignmentData)
        {
            if (consignmentData.ConsignmentDetails!.AssignToRoute.IsNotNullOrEmpty())
            {
                driver.GetElement(ConsignmentModalLocators.Text.AssignToRoute).SendKeys(consignmentData.ConsignmentDetails!.AssignToRoute!);
                driver.WaitAvailability(ConsignmentModalLocators.Body.ParentRoute);
                dropDownList.SelectByText(consignmentData.ConsignmentDetails!.AssignToRoute!);
            }
        }

        public void ClickSaveButton()
        {
            driver.GetElement(ConsignmentModalLocators.Button.Save).Click();
        }

        public bool IsSavedConsignmentMessageDisplayed()
        {
            return driver.GetElement(ConsignmentModalLocators.Message.Notification).IsElementTextContains("saved");
        }

        public void SelectCreateJob(ConsignmentData consignmentData)
        {
            driver.GetElement(ConsignmentModalLocators.Body.CreateJob).Click();

            if (driver.IsElementPresent(ConsignmentModalLocators.Body.JobInputList))
            {
                var jobData = consignmentData.ConsignmentDetails!.CreateJob!.ToLower().Trim();
                driver.WaitAvailability(ConsignmentModalLocators.Body.JobInputList);
                IWebElement ParentElement = driver.GetElement(ConsignmentModalLocators.Body.JobInputList);

                ParentElement!.GetElements(ConsignmentModalLocators.Body.CreateJobList)
                    .Where(e => e.Text.ToLower().Trim().Equals(jobData))
                    .First()
                    .Click();
            }
        }

        protected override bool EvaluateLoadedStatus()
        {
            bool IsConsignmentModalPresent = driver.IsElementPresent(ConsignmentModalLocators.Body.Consignment);

            return IsConsignmentModalPresent;
        }
    }
}
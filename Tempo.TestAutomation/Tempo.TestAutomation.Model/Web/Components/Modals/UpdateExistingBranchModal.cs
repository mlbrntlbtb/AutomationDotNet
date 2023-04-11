using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using Tempo.TestAutomation.Model.DTOs;
using Tempo.TestAutomation.Model.Web.Components.PageContainers;
using Tempo.TestAutomation.Model.Web.Locators.Modals;

namespace Tempo.TestAutomation.Model.Web.Components.Modals
{
    public class UpdateExistingBranchModal : TempoBasePage<UpdateExistingBranchModal>, ILoadable<UpdateExistingBranchModal>
    {
        private readonly IWebDriver driver;
        private readonly DropDownListContainer dropDownList;

        public UpdateExistingBranchModal(IWebDriver driver, DropDownListContainer dropDownList) : base(driver)
        {
            this.driver = driver;
            this.dropDownList = dropDownList;
        }

        public void ClickCancel()
        {
            driver.GetElement(UpdateExistingBranchModalLocators.Footer.Cancel).Click();
        }

        public void UpdateBranchDetails(BranchDetails branchDetails)
        {
            driver.GetElement(UpdateExistingBranchModalLocators.TabMenu.DetailsForm.Code).Clear();
            driver.GetElement(UpdateExistingBranchModalLocators.TabMenu.DetailsForm.Code).SendKeys(branchDetails.Code);
        }

        protected override bool EvaluateLoadedStatus()
        {
            bool isHeaderPresent = driver.IsElementPresent(UpdateExistingBranchModalLocators.Header.UpdateExistingBranch);
            return isHeaderPresent;
        }
    }
}
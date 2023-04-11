using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using Tempo.TestAutomation.Model.DTOs;
using Tempo.TestAutomation.Model.Web.Components.PageContainers;
using Tempo.TestAutomation.Model.Web.Locators.Modals;

namespace Tempo.TestAutomation.Model.Web.Components.Modals
{
    public class AddNewBranchModal : TempoBasePage<AddNewBranchModal>, ILoadable<AddNewBranchModal>
    {
        private readonly IWebDriver driver;
        private readonly DropDownListContainer dropDownList;

        public AddNewBranchModal(IWebDriver driver, DropDownListContainer dropDownList) : base(driver)
        {
            this.driver = driver;
            this.dropDownList = dropDownList;
        }

        public void AddNewBranchDetails(BranchDetails branchDetails)
        {
            driver.GetElement(AddNewBranchLocators.TabMenu.DetailsForm.ShortName).SendKeys(branchDetails.ShortName);
            driver.GetElement(AddNewBranchLocators.TabMenu.DetailsForm.Name).SendKeys(branchDetails.Name);
            driver.GetElement(AddNewBranchLocators.TabMenu.DetailsForm.Description).SendKeys(branchDetails.Description);
            driver.GetElement(AddNewBranchLocators.TabMenu.DetailsForm.Code).SendKeys(branchDetails.Code);
            driver.GetElement(AddNewBranchLocators.TabMenu.DetailsForm.Key).SendKeys(branchDetails.Key);
        }

        public void ClickCancel()
        {
            driver.GetElement(AddNewBranchLocators.Footer.Cancel).Click();
        }

        protected override bool EvaluateLoadedStatus()
        {
            bool isHeaderPresent = driver.IsElementPresent(AddNewBranchLocators.Header.AddNewBranch);
            return isHeaderPresent;
        }
    }
}
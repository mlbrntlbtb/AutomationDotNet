using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using Tempo.TestAutomation.Model.Web.Components.PageContainers;
using Tempo.TestAutomation.Model.Web.Locators.Modals;

namespace Tempo.TestAutomation.Model.Web.Components.Modals
{
    public class EditJobModal : TempoBasePage<EditJobModal>, ILoadable<EditJobModal>
    {
        private readonly IWebDriver driver;
        private readonly DropDownListContainer dropDownList;

        public EditJobModal(IWebDriver driver, DropDownListContainer dropDownList)
            : base(driver)
        {
            this.driver = driver;
            this.dropDownList = dropDownList;
        }

        public string GetHeaderJobID()
        {
            string headerText = driver.GetElement(EditJobModalLocators.EditJobModalFrame.Label.EditJobHeader).Text;
            int start = headerText.IndexOf("(") + 1;
            int end = headerText.IndexOf(")", start);

            return headerText.Substring(start, end - start);
        }

        protected override bool EvaluateLoadedStatus()
        {
            driver.WaitAvailability(EditJobModalLocators.Header.EditJob);
            bool IsEditJobHeaderPresent = driver.IsElementPresent(EditJobModalLocators.Header.EditJob);
            return IsEditJobHeaderPresent;
        }

        public void ClickCloseButton()
        {
            driver.GetElement(EditJobModalLocators.Button.Close).Click();
        }
    }
}
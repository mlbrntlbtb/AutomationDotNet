using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using Tempo.TestAutomation.Model.Web.Components.Object;
using Tempo.TestAutomation.Model.Web.Components.PageContainers;
using Tempo.TestAutomation.Model.Web.Locators.Dialogs;

namespace Tempo.TestAutomation.Model.Web.Components.Dialogs
{
    public class UpdateSettingsDialog : TempoBasePage<UpdateSettingsDialog>, ILoadable<UpdateSettingsDialog>
    {
        private readonly IWebDriver driver;
        private readonly DropDownListContainer dropDownList;
        private readonly LoadingWheel loadingWheel;

        public UpdateSettingsDialog(IWebDriver driver, DropDownListContainer dropDownList, LoadingWheel loadingWheel) : base(driver)
        {
            this.driver = driver;
            this.dropDownList = dropDownList;
            this.loadingWheel = loadingWheel;
        }

        public void ClickSaveButton()
        {
            driver.GetElement(UpdateSettingsDialogLocators.Button.Save).Click();
        }

        public string GetCreateConsignmentOrderValue()
        {
            return driver.GetElement(UpdateSettingsDialogLocators.List.CreateConsignmentOrder).Text.Trim();
        }

        public void SelectCreateConsignmentOrder(string referenceText)
        {
            driver.GetElement(UpdateSettingsDialogLocators.List.CreateConsignmentOrder).Click();
            dropDownList.SelectByText(referenceText);
        }

        protected override bool EvaluateLoadedStatus()
        {
            loadingWheel.WaitToDisappear();
            bool IsHeaderDisplayed = driver.IsElementPresent(UpdateSettingsDialogLocators.Label.Header);
            return IsHeaderDisplayed;
        }
    }
}
using Datacom.TestAutomation.Web.Selenium;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Tempo.TestAutomation.Model.Web.Locators.Dialogs;

namespace Tempo.TestAutomation.Model.Web.Components.Dialogs
{
    public class ConfirmationDialog : TempoBasePage<ConfirmationDialog>, ILoadable<ConfirmationDialog>
    {
        private readonly IWebDriver driver;

        public ConfirmationDialog(IWebDriver driver)
            : base(driver)
        {
            this.driver = driver;
        }

        public void ClickNoButton()
        {
            driver.GetElement(ConfirmationDialogLocators.Button.No).Click();
        }

        public void ClickYesButton()
        {
            driver.GetElement(ConfirmationDialogLocators.Button.Yes).Click();
        }

        protected override bool EvaluateLoadedStatus()
        {
            bool isYesButtonPresent = driver.IsElementPresent(ConfirmationDialogLocators.Button.Yes);
            bool isNoButtonPresent = driver.IsElementPresent(ConfirmationDialogLocators.Button.No);
            return isYesButtonPresent && isNoButtonPresent;
        }
    }
}
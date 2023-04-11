using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;

namespace Tempo.TestAutomation.Model.Web.Components.Dialogs
{
    public class EditJobConfirmationDialog : TempoBasePage<EditJobConfirmationDialog>, ILoadable<EditJobConfirmationDialog>
    {
        private readonly IWebDriver driver;

        public EditJobConfirmationDialog(IWebDriver driver)
            : base(driver)
        {
            this.driver = driver;
        }

        protected override bool EvaluateLoadedStatus()
        {
            return false;
        }
    }
}
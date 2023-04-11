using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;

namespace Tempo.TestAutomation.Model.Web.Components.Dialogs
{
    public class StatusUpdateDialog : TempoBasePage<StatusUpdateDialog>, ILoadable<StatusUpdateDialog>
    {
        private readonly IWebDriver driver;

        public StatusUpdateDialog(IWebDriver driver)
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
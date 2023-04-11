using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;

namespace Tempo.TestAutomation.Model.Web.Components.Dialogs
{
    public class ConsignmentSuccessDialog : TempoBasePage<ConsignmentSuccessDialog>, ILoadable<ConsignmentSuccessDialog>
    {
        private readonly IWebDriver driver;

        public ConsignmentSuccessDialog(IWebDriver driver)
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
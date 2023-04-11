using Datacom.TestAutomation.Web.Selenium;
using OpenQA.Selenium;
using Tempo.TestAutomation.Model.Web.Components.Object;
using Tempo.TestAutomation.Model.Web.Components.PageContainers;

namespace Tempo.TestAutomation.Model.Web.Components.Pages
{
    public class RecurringJobsPage : TempoBasePage<RecurringJobsPage>, ILoadable<RecurringJobsPage>
    {
        private readonly IWebDriver driver;
        private readonly DropDownListContainer dropDownList;
        private readonly LoadingWheel loadingWheel;

        public RecurringJobsPage(IWebDriver driver, DropDownListContainer dropDownList, LoadingWheel loadingWheel)
            : base(driver)
        {
            this.driver = driver;
            this.dropDownList = dropDownList;
            this.loadingWheel = loadingWheel;
        }

        protected override bool EvaluateLoadedStatus()
        {
            loadingWheel.WaitToDisappear();

            return false;
        }
    }
}